using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsCountryData
    {
        public static DataTable GetAllCountry()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM Countries order by CountryName";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsCountryData", "GetAllCountry Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool FindCountryByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = "SELECT * FROM Countries WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsCountryData", "FindCountryByID Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool FindCountryByName(string CountryName, ref int CountryID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = "SELECT * FROM Countries WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryID = (int)reader["CountryID"];
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsEventLogData EvenLog = clsEventLogData.SetEvent("clsCountryData", "FindCountryByName Error :" + e.Message, clsEventLogData.enEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}
