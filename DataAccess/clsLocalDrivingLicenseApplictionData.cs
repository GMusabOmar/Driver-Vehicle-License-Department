using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsLocalDrivingLicenseApplictionData
    {
        public static bool FoundByID(int LocalDrivingLicenseApplicationID,
            ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT * FROM LocalDrivingLicenseApplications 
                            WHERE LocalDrivingLicenseApplicationID = @ID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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

        public static bool FoundByApplicationID(int ApplicationID,
             ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT * FROM LocalDrivingLicenseApplications 
                            WHERE ApplicationID = @ApplicationID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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

        public static DataTable GetAllDrivingLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM LocalDrivingLicenseApplications_View
                            order by ApplicationDate Desc;";
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

        public static int AddNewLocalDrivingLicense(int ApplicationID, int LicenseClassID)
        {
            int NewID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[LocalDrivingLicenseApplications]
                                       ([ApplicationID]
                                       ,[LicenseClassID])
                                 VALUES
                                       (@ApplicationID, @LicenseClassID);
		                               SELECT SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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

        public static bool UpdateLocalDrivingLicense(int LocalDrivingLicenseApplicationID,
            int ApplicationID, int LicenseClassID)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[LocalDrivingLicenseApplications]
                               SET [ApplicationID] = @ApplicationID
                                  ,[LicenseClassID] = @LicenseClassID
                             WHERE 
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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

        public static bool DeleteLocalDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            DELETE FROM [dbo].[LocalDrivingLicenseApplications]
                            WHERE
                            LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT TOP 1 TestResult
                            FROM LocalDrivingLicenseApplications L
                            INNER JOIN TestAppointments TA
                            ON 
                            L.LocalDrivingLicenseApplicationID = TA.LocalDrivingLicenseApplicationID
                            INNER JOIN Tests T
                            ON T.TestAppointmentID = TA.TestAppointmentID
                            WHERE 
                            L.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND TA.TestTypeID = @TestTypeID
                            ORDER BY TA.TestAppointmentID DESC;                            
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if(Result != null && bool.TryParse(Result.ToString(), out bool Found))
                    isFound = Found;
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

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT TOP 1 TestResult = 1
                            FROM LocalDrivingLicenseApplications L
                            INNER JOIN TestAppointments TA
                            ON 
                            L.LocalDrivingLicenseApplicationID = TA.LocalDrivingLicenseApplicationID
                            INNER JOIN Tests T
                            ON T.TestAppointmentID = TA.TestAppointmentID
                            WHERE 
                            L.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND TA.TestTypeID = @TestTypeID
                            ORDER BY TA.TestAppointmentID DESC;                            
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null)
                    isFound = true;
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

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte TotalTrial = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)                           
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && byte.TryParse(Result.ToString(), out byte ID))
                    TotalTrial = ID;
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return TotalTrial;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT TOP 1 TestResult = 1
                            FROM LocalDrivingLicenseApplications L
                            INNER JOIN TestAppointments TA
                            ON 
                            L.LocalDrivingLicenseApplicationID = TA.LocalDrivingLicenseApplicationID
                            INNER JOIN Tests T
                            ON T.TestAppointmentID = TA.TestAppointmentID
                            WHERE 
                            L.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND TA.TestTypeID = @TestTypeID
                            ORDER BY TA.TestAppointmentID DESC;                            
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null)
                    isFound = true;
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
