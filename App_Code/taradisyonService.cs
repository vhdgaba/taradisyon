using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;    
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Taradisyon
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    public class taradisyonService : ItaradisyonService
    {
        public void DoWork()
        {
        }

        // Code for logging in user.
        // Returns true if login successful, false if account not found.
        public bool Login(string EmailAddress, string Password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    List<User> data = new List<User>();
                    string query = "SELECT ID, EmailAddress, Password" +
                        "FROM dbo.User" +
                        "WHERE EmailAddress = @email AND Password = @password";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@email", SqlDbType.Text).Value = EmailAddress;
                    command.Parameters.Add("@password", SqlDbType.Text).Value = Password;
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                            User input = new User();
                            input.ID = reader.GetInt32(0);
                            input.EmailAddress = reader.GetString(1);
                            input.Password = reader.GetString(2);
                            data.Add(input);
                        }
                    
                    // Query ID and Password using Email, if results found, set to User item and add to query List

                    if (data.Count() != 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
        }

        // Code for signing up.
        // Returns true for successful account creation, false otherwise
        public bool SignUp(string FirstName, string LastName, char Gender, string EmailAddress, string Password, DateTime Birthdate, string Nationality, int Point)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    // Query for existing entry using Email, store in query
                    string query = "SELECT *" +
                        "FROM dbo.User" +
                        "WHERE EmailAddress = @email";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@email", SqlDbType.Text).Value = EmailAddress;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            return false;
                        else
                        {
                            query = "INSERT INTO dbo.User (LastName, FirstName, Gender, EmailAddress, Password, Birthdate, Nationality, Points)" +
                                "VALUES (@fname, @lname, @gender, @email, @password, @bday, @nationality, 0)";

                            command = new SqlCommand(query, connection);
                            command.Parameters.Add("@fname", SqlDbType.Text).Value = FirstName;
                            command.Parameters.Add("@lname", SqlDbType.Text).Value = LastName;
                            command.Parameters.Add("@gender", SqlDbType.Char).Value = Gender;
                            command.Parameters.Add("@email", SqlDbType.Text).Value = EmailAddress;
                            command.Parameters.Add("@password", SqlDbType.Text).Value = Password;
                            command.Parameters.Add("@bday", SqlDbType.Date).Value = Birthdate;
                            command.Parameters.Add("@nationality", SqlDbType.Text).Value = Nationality;
                        }

                        return false;
                    }
                }
                catch
                {
                    return false;
                }
        }

        public void GetUserData(int ID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "SELECT *" +
                        "FROM dbo.User" +
                        "WHERE ID = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {
                                User input = new User();
                                input.ID = reader.GetInt32(0);
                                input.FirstName = reader.GetString(1);
                                input.LastName = reader.GetString(2);
                                input.Gender = reader.GetChar(3);
                                input.EmailAddress = reader.GetString(4);
                                input.Password = reader.GetString(5);
                                input.BirthDate = reader.GetDateTime(6);
                                input.Nationality = reader.GetString(7);
                                input.Point = reader.GetInt32(8);
                            }
                    }
                }
                catch
                {
                }
        }
    }
}


    