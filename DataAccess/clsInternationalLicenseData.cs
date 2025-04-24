using System;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess
{
    public class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseByID(int InternationalLicenseID,
            ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive,
            ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM InternationalLicenses
                            WHERE InternationalLicenseID = @InternationalLicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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
        public static bool GetInternationalLicenseByDriverID(int DriverID,
            ref int InternationalLicenseID,
            ref int ApplicationID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive,
            ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM InternationalLicenses
                            WHERE DriverID = @DriverID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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
        public static DataTable GetAllInternationalLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT [InternationalLicenseID]
                            ,[ApplicationID]
                            ,[DriverID]
                            ,[IssuedUsingLocalLicenseID]
                            ,[IssueDate]
                            ,[ExpirationDate]
                            ,[IsActive]
                            FROM [dbo].[InternationalLicenses]";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
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
        public static DataTable GetAllInternationalLicenseByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT [InternationalLicenseID]
                            ,[ApplicationID]
                            ,[DriverID]
                            ,[IssuedUsingLocalLicenseID]
                            ,[IssueDate]
                            ,[ExpirationDate]
                            ,[IsActive]
                            FROM [dbo].[InternationalLicenses]
                            WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, 
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, 
            bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[InternationalLicenses]
                            SET [IsActive] = 0
                            WHERE DriverID = @DriverID;

                            INSERT INTO [dbo].[InternationalLicenses]
                            ([ApplicationID]
                            ,[DriverID]
                            ,[IssuedUsingLocalLicenseID]
                            ,[IssueDate]
                            ,[ExpirationDate]
                            ,[IsActive]
                            ,[CreatedByUserID])
                            VALUES
                            (@ApplicationID
                            ,@DriverID
                            ,@IssuedUsingLocalLicenseID
                            ,@IssueDate
                            ,@ExpirationDate
                            ,@IsActive
                            ,@CreatedByUserID)
                            SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    InternationalLicenseID = ID;
                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return InternationalLicenseID;
        }
        public static bool UpdateInternationalLicense(int InternationalLicenseID,
            int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, 
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[InternationalLicenses]
                            SET [ApplicationID] = @ApplicationID
                            ,[DriverID] = @DriverID
                            ,[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID
                            ,[IssueDate] = @IssueDate
                            ,[ExpirationDate] = @ExpirationDate
                            ,[IsActive] = @IsActive
                            ,[CreatedByUserID] = @CreatedByUserID
                            WHERE InternationalLicenseID = @InternationalLicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
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
        public static bool IsInternationalLicenseExists(int DriverID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT InternationalLicenseID FROM InternationalLicenses
                            WHERE DriverID = @DriverID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.Read();
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
    }
}
