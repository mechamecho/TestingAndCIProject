using System.Collections.Generic;
using System.Data.SqlClient;



namespace CustomMethodsForControls
{
    public class DataEntry
    {
        public DataEntry()
        {

        }

         SqlConnection connection;
         string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Engineer\Desktop\TestingAndCIProject\CustomMethodsForControls\InputTesting.mdf;Integrated Security=True";

        public void PopulateLoginTable(string username, string password)
        {
            string query="INSERT INTO LoginInfo(Username, Password) VALUES (@Username, @Password)";
            using (connection = new SqlConnection(ConnectionString))
            using(SqlCommand command= new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteScalar();
            }
            
        }

        public void PopulateUserFormTable(string title, string initial, string firstname, string middlename, string gender, string [] languages)
        {
            string query = "INSERT INTO UserForm (Title, Initials, Firstname, MiddleName, Gender, Language1, Language2)"+"" +
                "VALUES(@Title, @Initial, @Firstname, @Middlename,@Gender, @Language1, @Language2)";
            using (connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Initial", initial);
                command.Parameters.AddWithValue("@Firstname", firstname);
                command.Parameters.AddWithValue("@Middlename", middlename);
                command.Parameters.AddWithValue("@Gender", gender);
                if (languages.Length == 2)
                {
                    command.Parameters.AddWithValue("@Language1", languages[0]);
                    command.Parameters.AddWithValue("@Language2", languages[1]);

                }
                else
                {
                    command.Parameters.AddWithValue("@Language1", languages[0]);
                }

                command.ExecuteScalar();
            }

        }
    }
}
