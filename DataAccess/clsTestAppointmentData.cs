using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
            ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref decimal PaidFees,
            ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM TestAppointments
                            WHERE TestAppointmentID = @TestAppointmentID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                        RetakeTestApplicationID = -1;
                }
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

        public static bool GetLastTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
            ref int TestAppointmentID, ref DateTime AppointmentDate, ref decimal PaidFees,
            ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT top 1 * FROM TestAppointments
                            WHERE TestTypeID = @TestTypeID
                            AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;                            
                            order by TestAppointmentID Desc";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                        RetakeTestApplicationID = -1;
                }
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

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM TestAppointments_View order by AppointmentDate Desc;";
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

        public static DataTable GetApplicationTestAppointmentsPerTestType(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                            FROM TestAppointments
                            WHERE TestTypeID = @TestTypeID
                            AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            order by TestAppointmentID desc;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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

        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees,
            int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int NewID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[TestAppointments]
                                       ([TestTypeID]
                                       ,[LocalDrivingLicenseApplicationID]
                                       ,[AppointmentDate]
                                       ,[PaidFees]
                                       ,[CreatedByUserID]
                                       ,[IsLocked]
                                       ,[RetakeTestApplicationID])
                                 VALUES
                                       (@TestTypeID
                                       ,@LocalDrivingLicenseApplicationID
                                       ,@AppointmentDate
                                       ,@PaidFees
                                       ,@CreatedByUserID
                                       ,@IsLocked
                                       ,@RetakeTestApplicationID)
		                               SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("IsLocked", IsLocked);
            if(RetakeTestApplicationID != -1)
                command.Parameters.AddWithValue("RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("RetakeTestApplicationID", DBNull.Value);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    NewID = ID;
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return NewID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, 
            int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[TestAppointments]
                               SET [TestTypeID] = @TestTypeID
                                  ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID
                                  ,[AppointmentDate] = @AppointmentDate
                                  ,[PaidFees] = @PaidFees
                                  ,[CreatedByUserID] = @CreatedByUserID
                                  ,[IsLocked] = @IsLocked
                                  ,[RetakeTestApplicationID] = @RetakeTestApplicationID
                             WHERE TestAppointmentID = @TestAppointmentID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if (RetakeTestApplicationID != -1)
                command.Parameters.AddWithValue("RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("RetakeTestApplicationID", DBNull.Value);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
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

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"DELETE FROM [dbo].[TestAppointments]
                             WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
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

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    TestID = ID;
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return TestID;
        }

    }
}
