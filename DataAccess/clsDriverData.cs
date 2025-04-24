using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsDriverData
    {
        public static bool GetDriverInfoByID(int DriverID, ref int PersonID,
            ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT * FROM Drivers WHERE DriverID = @DriverID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDriverData", "GetDriverInfoByID Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID,
            ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT * FROM Drivers WHERE PersonID = @PersonID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
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
        public static DataTable GetAllDriver()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM Drivers_View order by FullName;";
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
        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[Drivers] ([PersonID] ,[CreatedByUserID] ,[CreatedDate])
                            VALUES (@PersonID ,@CreatedByUserID ,@CreatedDate)
                            SELECT SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    DriverID = ID;
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return DriverID;
        }
        public static bool UpdateDriver(int DriverID, int PersonID, 
            int CreatedByUserID, DateTime CreatedDate)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[Drivers]
                            SET [PersonID] = @PersonID
                            ,[CreatedByUserID] = @CreatedByUserID
                            ,[CreatedDate] = @CreatedDate
                             WHERE DriverID = @DriverID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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
