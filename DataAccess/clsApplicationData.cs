using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsApplicationData
    {
        public static bool GetApplicationInfoByID(int ApplicationID,
            ref int ApplicantPersonID, ref DateTime ApplicationDate,
            ref int ApplicationTypeID, ref byte ApplicationStatus,
            ref DateTime LastStatusDate, ref float PaidFees,
            ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT * FROM Applications WHERE ApplicationID = @ApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch(Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "GetApplicationInfoByID Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM Applications ORDER BY ApplicationDate DESC;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "GetAllApplications Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID)
        {
            int newID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[Applications]
                                       ([ApplicantPersonID]
                                       ,[ApplicationDate]
                                       ,[ApplicationTypeID]
                                       ,[ApplicationStatus]
                                       ,[LastStatusDate]
                                       ,[PaidFees]
                                       ,[CreatedByUserID])
                            VALUES
                                       (@ApplicantPersonID 
                                       ,@ApplicationDate
                                       ,@ApplicationTypeID
                                       ,@ApplicationStatus
                                       ,@LastStatusDate
                                       ,@PaidFees
                                       ,@CreatedByUserID)
                                        SELECT SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    newID = ID;
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "AddNewApplication Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return newID;
        }
        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, 
            DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, 
            DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[Applications]
                               SET [ApplicantPersonID] = @ApplicantPersonID
                                  ,[ApplicationDate] = @ApplicationDate
                                  ,[ApplicationTypeID] = @ApplicationTypeID
                                  ,[ApplicationStatus] = @ApplicationStatus
                                  ,[LastStatusDate] = @LastStatusDate
                                  ,[PaidFees] = @PaidFees
                                  ,[CreatedByUserID] = @CreatedByUserID
                             WHERE ApplicationID = @ApplicationID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "UpdateApplication Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"DELETE FROM [dbo].[Applications]
                            WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "DeleteApplication Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }
        public static bool IsApplicationExists(int ApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT X = 5 FROM Applications WHERE ApplicationID = @ApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "IsApplicationExists Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool UpdateStatus(int ApplicationID, byte ApplicationStatus)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[Applications]
                               SET ApplicationStatus = @ApplicationStatus,
                                LastStatusDate = @LastStatusDate
                             WHERE ApplicationID = @ApplicationID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "UpdateStatus Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }
        public static int GetActiveApplicationID(int ApplicantPersonID, int ApplicationTypeID)
        {
            int NewID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT ActiveApplicationID = ApplicationID FROM Applications
                            WHERE ApplicantPersonID = @ApplicantPersonID
                            AND ApplicationTypeID = @ApplicationTypeID
                            AND ApplicationStatus = 1;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    NewID = ID;
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "GetActiveApplicationID Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return NewID;
        }
        public static bool DoesPersonHaveActiveApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            return GetActiveApplicationID(ApplicantPersonID, ApplicationTypeID) != -1;
        }
        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int NewID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT Applications.ApplicationID AS ActiveApplicationID
                            FROM Applications 
                            INNER JOIN
                            LocalDrivingLicenseApplications 
                            ON 
                            Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE (Applications.ApplicantPersonID = @ApplicantPersonID) 
                            AND (Applications.ApplicationTypeID = @ApplicationTypeID) 
                            AND (Applications.ApplicationStatus = 1)
                            AND LicenseClassID = @LicenseClassID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    NewID = ID;
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationData", "GetActiveApplicationIDForLicenseClass Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return NewID;
        }

    }
}
