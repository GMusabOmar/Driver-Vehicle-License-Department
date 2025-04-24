using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsDetainedLicenseData
    {
        public static bool FindByID(int DetainID, ref int LicenseID, ref DateTime DetainDate,
            ref float FineFees, ref int CreatedByUserID, ref bool IsReleased,
            ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM [dbo].[DetainedLicenses]
                             WHERE DetainID = @DetainID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    ReleaseDate = (DateTime)reader["ReleaseDate"];
                    ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDetainedLicenseData", "FindByID Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool FindByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate,
            ref float FineFees, ref int CreatedByUserID, ref bool IsReleased,
            ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM [dbo].[DetainedLicenses]
                             WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    ReleaseDate = (DateTime)reader["ReleaseDate"];
                    ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDetainedLicenseData", "FindByLicenseID Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static DataTable GetAllDetainedLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM DetainedLicenses_View";
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
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDetainedLicenseData", "GetAllDetainedLicense Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID)
        {
            int DetainID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[DetainedLicenses]
                            ([LicenseID]
                            ,[DetainDate]
                            ,[FineFees]
                            ,[CreatedByUserID]
                            )
                            VALUES
                            (@LicenseID
                            ,@DetainDate
                            ,@FineFees
                            ,@CreatedByUserID
                            )
                            SELECT SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    DetainID = ID;
                }
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDetainedLicenseData", "AddNewDetainedLicense Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return DetainID;
        }
        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, 
            DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[DetainedLicenses]
                            SET [LicenseID] = @LicenseID
                            ,[DetainDate] = @DetainDate
                            ,[FineFees] = @FineFees
                            ,[CreatedByUserID] = @CreatedByUserID
                            WHERE DetainID = @DetainID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDetainedLicenseData", "UpdateDetainedLicense Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }
        public static bool ReleaseDetainedLicense(int DetainID,
                 int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDetainedLicenseData", "ReleaseDetainedLicense Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select IsDetained=1 
                            from detainedLicenses 
                            where 
                            LicenseID=@LicenseID 
                            and IsReleased=0;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    IsDetained = Convert.ToBoolean(result);
                }
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsDetainedLicenseData", "IsLicenseDetained Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return IsDetained;
        }
    }
}
