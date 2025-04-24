using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsTestTypeData
    {
        public static bool Found(int TestTypeID,
            ref string TestTypeTitle, ref string TestTypeDescription,
            ref decimal TestTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal)reader["TestTypeFees"];
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
        public static int AddNewTest(string TestTypeTitle, string TestTypeDescription,
            decimal TestTypeFees)
        {
            int NewID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            INSERT INTO [dbo].[TestTypes]
                           ([TestTypeTitle]
                           ,[TestTypeDescription]
                           ,[TestTypeFees])
                            VALUES
                           (@TestTypeTitle, @TestTypeDescription, @TestTypeFees);
		                    SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    NewID = ID;
                }
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
        public static bool Update(int TestTypeID, string TestTypeTitle, 
            string TestTypeDescription,decimal TestTypeFees)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"
                            UPDATE [dbo].[TestTypes]
                               SET [TestTypeTitle] = @TestTypeTitle
                                  ,[TestTypeDescription] = @TestTypeDescription
                                  ,[TestTypeFees] = @TestTypeFees
                             WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
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
        public static DataTable GetAllTest()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"SELECT * FROM TestTypes ORDER BY TestTypeID;";
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
            catch
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
