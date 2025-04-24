using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsApplicationTypeData
    {
        public static bool Find(int ApplicationTypeID, ref string ApplicationTypeTitle,
            ref float ApplicationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationTypeData", "Find Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int AddNewApplication(string ApplicationTypeTitle,
            float ApplicationFees)
        {
            int NewID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[ApplicationTypes]
                            ([ApplicationTypeTitle] ,[ApplicationFees])
                            VALUES
                            ([@ApplicationTypeTitle] ,[@ApplicationFees])
                            SELECT SCOPE_IDENTITY();
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                    NewID = ID;
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationTypeData", "AddNewApplication Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return NewID;
        }

        public static bool UpdateApplication(int ApplicationTypeID, 
            string ApplicationTypeTitle, float ApplicationFees)
        {
            int rowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                        UPDATE [dbo].[ApplicationTypes]
                           SET [ApplicationTypeTitle] = @ApplicationTypeTitle
                              ,[ApplicationFees] = @ApplicationFees
                         WHERE ApplicationTypeID = @ApplicationTypeID;
                            ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationTypeData", "UpdateApplication Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }

        public static DataTable GetAllApplication()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = "SELECT * FROM ApplicationTypes ORDER BY ApplicationTypeTitle;";
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
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsApplicationTypeData", "GetAllApplication Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

    }
}
