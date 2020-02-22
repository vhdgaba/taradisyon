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

        public bool Login(string EmailAddress, string Password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;

            string query = "SELECT ID, EmailAddress, Password" +
                "FROM dbo.User" +
                "WHERE EmailAddress = @Email AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("Email", SqlDbType.Varchar, 30).Value = EmailAddress;
                command.Parameters.Add("Password", SqlDbType.Char, 64).Value = Password;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UserID = reader.GetInt32(0);
                    }
                }
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    connection.Close();
                    
                    query = "SELECT ID, EmailAddress, Password" +
                    "FROM dbo.Administrator" +
                    "WHERE EmailAddress = @Email AND Password = @Password";
        
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.Add("Email", SqlDbType.Varchar, 30).Value = EmailAddress;
                        command.Parameters.Add("Password", SqlDbType.Char, 64).Value = Password;
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {
                                admin.UserID = reader.GetInt32(0);
                            }
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        connection.Close();
                    }
                    return false;
                }
            }
        }

        public bool SignUp(string EmailAddress)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
            string query = "SELECT *" +
                    "FROM dbo.User" +
                    "WHERE EmailAddress = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Email", SqlDbType.Varchar, 30).Value = EmailAddress;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        return false;
                    else
                    {
                        connection.Close();
                        query = "INSERT INTO dbo.User (LastName, FirstName, Gender, EmailAddress, Password, Birthdate, Nationality, Points)" +
                            "VALUES (@FirstName, @LastName, @Gender, @Email, @Password, @Birthdate, @NationalityID, @Point)";
                        
                        connection.Open();
                        command = new SqlCommand(query, connection);
                        command.Parameters.Add("@FirstName", SqlDbType.Varchar, 20).Value = FirstName;
                        command.Parameters.Add("@LastName", SqlDbType.Varchar, 20).Value = LastName;
                        command.Parameters.Add("@Gender", SqlDbType.Char).Value = Gender;
                        command.Parameters.Add("@EmailAddress", SqlDbType.Varchar, 30).Value = EmailAddress;
                        command.Parameters.Add("@Password", SqlDbType.Char, 64).Value = Password;
                        command.Parameters.Add("@Birthdate", SqlDbType.Date).Value = Birthdate;
                        command.Parameters.Add("@NationalityID", SqlDbType.Int).Value = Nationality;
                        command.Parameters.Add("@Point", SqlDbType.Int).Value = 0;
                        command.Execute.Nonquery();
                        connection.Close();
                    }
                    return false;
                }
            }
        }
    }

    public List<string> GetUserData(string UserID)
    {
        List<string> data = new List<string>();
        string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT *" +
                "FROM dbo.User" +
                "WHERE ID = @ID";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = UserID;
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        User user = new User();
                        user.ID = reader.GetInt32(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.Gender = reader.GetChar(3);
                        user.EmailAddress = reader.GetString(4);
                        user.Password = reader.GetString(5);
                        user.BirthDate = reader.GetDateTime(6);
                        user.Nationality = reader.GetString(7);
                        user.Point = reader.GetInt32(8);
                        data.Add(user);
                    }
            }
            conntection.Close()
        }
    }

    public List<Challenge> GetChallengeByAdministrator()
    {
        List<Challenge> challenge = new List<Challenge>();
        string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT *" +
                "FROM dbo.Challenge" +
                "WHERE AdministratorID = @AdministratorID";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@AdministratorID", SqlDbType.Int).Value = admin.ID;
                
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        Challenge dare = new Challenge();
                        dare.ID = reader.GetInt32(0);
                        dare.Title = reader.GetString(1);
                        dare.Description = reader.GetString(2);
                        dare.CategoryID = reader.GetInt32(3);
                        dare.LocationID = reader.GetInt32(4);
                        dare.AdministratorID = reader.GetInt32(5);
                        dare.Emblem = reader.GetDateString(6);
                        dare.Point = reader.GetInt32(7);
                        dare.Picture = reader.GetString(8);
                        challenge.Add(dare);
                    }
            }
            connection.Close();
        }
    }

    public List<Challenge> GetChallengeByLocationCategory(string CategoryID, string LocationID)
    {
        List<Challenge> challenge = new List<Challenge>();
        string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT *" +
                "FROM dbo.Challenge" +
                "WHERE LocationID = @LocationID AND CategoryID = @CategoryID";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;
                command.Parameters.Add("@LocationID", SqlDbType.Int).Value = LocationID;
                
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        Challenge dare = new Challenge();
                        dare.ID = reader.GetInt32(0);
                        dare.Title = reader.GetString(1);
                        dare.Description = reader.GetString(2);
                        dare.CategoryID = reader.GetInt32(3);
                        dare.LocationID = reader.GetInt32(4);
                        dare.AdministratorID = reader.GetInt32(5);
                        dare.Emblem = reader.GetDateString(6);
                        dare.Point = reader.GetInt32(7);
                        dare.Picture = reader.GetString(8);
                        challenge.Add(dare);
                    }
            }
            connection.Close();
        }
    }
}


    
