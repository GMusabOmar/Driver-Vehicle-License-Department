using System;
using System.Data.SqlClient;


namespace DataAccess
{
    public class clsSettingData
    {
        public static bool FoundByID(int SettingID, ref int InternationalExpirFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            SELECT TOP 1 SettingID ,InternationalExpirFees
                            FROM Setting WHERE SettingID = @SettingID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@SettingID", SettingID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    InternationalExpirFees = (int)reader["InternationalExpirFees"];
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
    }
}
