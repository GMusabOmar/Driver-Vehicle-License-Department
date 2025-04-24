using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsLicenseClassData
    {
        public static bool GetLicenseClassByID(int LicenseClassID, ref string ClassName,
            ref string ClassDescription, ref byte MinimumAllowedAge,
            ref byte DefaultValidityLength, ref decimal ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = (decimal)reader["ClassFees"];
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetLicenseClassByClassName(string ClassName, ref int LicenseClassID,
            ref string ClassDescription, ref byte MinimumAllowedAge,
            ref byte DefaultValidityLength, ref decimal ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM LicenseClasses WHERE ClassName = @ClassName;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = (decimal)reader["ClassFees"];
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static DataTable GetAllLicenseClass()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM LicenseClasses order by ClassName;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static int AddNewLicenseClass(string ClassName,
            string ClassDescription, byte MinimumAllowedAge,
            byte DefaultValidityLength, decimal ClassFees)
        {
            int LicenseClassID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[LicenseClasses]
                            ([ClassName]
                            ,[ClassDescription]
                            ,[MinimumAllowedAge]
                            ,[DefaultValidityLength]
                            ,[ClassFees])
                            VALUES
                            (@ClassName
                            ,@ClassDescription
                            ,@MinimumAllowedAge
                            ,@DefaultValidityLength
                            ,@ClassFees)
                            SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID)) {
                    LicenseClassID = ID;
                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return LicenseClassID;
        }
        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName,
            string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength,
            decimal ClassFees)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[LicenseClasses]
                            SET [ClassName] = @ClassName
                            ,[ClassDescription] = @ClassDescription
                            ,[MinimumAllowedAge] = @MinimumAllowedAge
                            ,[DefaultValidityLength] = @DefaultValidityLength
                            ,[ClassFees] = @ClassFees
                            WHERE LicenseClassID = @LicenseClassID
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }

    }
}
