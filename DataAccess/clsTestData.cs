using System;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class clsTestData
    {
        public static bool GetTestByID(int TestID, ref int TestAppointmentID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM Tests WHERE TestID = @TestID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";
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
        public static bool GetLastTestByPersonAndLicenseClassAndTestAppointmentID(int PersonID, 
            int LicenseClassID, int TestTypeID, ref int TestAppointmentID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT TOP 1   Tests.*, Applications.ApplicantPersonID    
                            FROM LocalDrivingLicenseApplications INNER JOIN
                            TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                            Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN
                            Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                            WHERE ApplicantPersonID = @PersonID
                            AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            AND TestAppointments.TestTypeID = @TestTypeID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";
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
        public static DataTable GetAllTest()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM Tests order by TestID;";
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
        public static int AddNewTest(int TestAppointmentID, bool TestResult, 
            string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[Tests]
                            ([TestAppointmentID],[TestResult],[Notes],[CreatedByUserID])
                            VALUES
                            (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);

                            UPDATE TestAppointments
                            SET TestAppointments.IsLocked = 1 WHERE TestAppointments.TestAppointmentID = @TestAppointmentID
		   
                            SELECT SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query , connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if(Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult,
            string Notes, int CreatedByUserID)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[Tests]
                            SET [TestAppointmentID] = @TestAppointmentID
                                ,[TestResult] = @TestResult
                                ,[Notes] = @Notes
                                ,[CreatedByUserID] = @CreatedByUserID
                            WHERE TestID = @TestID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestID", TestID);
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
        public static byte GetPassTest(int LocalDrivingLicenseApplicationID)
        {
            byte count = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT Count = COUNT(TA.TestTypeID)
                            FROM Tests T
                            INNER JOIN TestAppointments TA
                            ON TA.TestAppointmentID = T.TestAppointmentID
                            WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND T.TestResult = 1
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && byte.TryParse(Result.ToString(), out byte NumCount))
                    count =  NumCount;
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return count;
        }

    }
}
