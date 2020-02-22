// Classes for Account related functions

using System;
using System.Text;
using System.Collections.Generic;

namespace Account
{
    class Token
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public int ChallengeID { get; set; }
        public bool isClaimed { get; set; }
        public DateTime ExpiryTime { get; set; } // HH:MM:SS
    }

    class Challenge
    {
        public int ChallengeID { get; set; }

        // Generates token and
        public string GenerateToken(string ChallengeID)
        {
            ChallengeToken newToken = new ChallengeToken();
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 6; i++)
            {
                if (random.NextDouble() > 0.5)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                else
                {
                    ch = Convert.ToChar(random.Next(10) + 48);
                    builder.Append(ch);
                }

            }
            newToken.Token = builder.ToString();
            newToken.ExpiryTime = DateTime.Now.AddMinutes(3);

            // Insert Token to database
            string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO dbo.User (ChallengeID, Token, ExpirationDate, Claimed)" +
                            "VALUES (@ChallengeID, @Token, @ExpirationDate, @Claimed)";
                
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("ChallengeID", SqlDbType.Int).Value = ChallengeID;
                    command.Parameters.Add("Token", SqlDbType.Char, 6).Value = newToken.Token;
                    command.Parameters.Add("ExpirationDate", SqlDbType.DateTime).Value = newToken.ExpiryTime;
                    command.Parameters.Add("Claimed", SqlDbType.Int).Value = 0;
                    command.Execute.Nonquery();
                    connection.Close();
                 }
              }
              return builder.ToString();
        }
    }

    class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public int RewardPoints { get; set; }

        // Code for logging in user.
        // Returns true if login successful, false if account not found.
        public string Login()
        {
            List<User> query = new List<User>();

            // Query ID and Password using Email, if results found, set to User item and add to query List

            if (query.Count() != 0)
            {
                foreach (User check in query)
                {
                    if (check.Password == Password)
                    {
                        // Query all User data using check.ID

                        // Insert mo nalang kung asan scope ng reader object mo yung code baba neto, pati index
                        FirstName = reader[0];
                        LastName = reader[0];
                        MiddleInitial = reader[0];
                        Gender = reader[0];
                        Email = reader[0];
                        Password = reader[0];
                        BirthDate = reader[0];
                        Nationality = reader[0];
                        RewardPoints = reader[0];

                        return "true";
                    }
                }
            }
            return "false";
        }

        // Code for signing up.
        // Returns true for successful account creation, false otherwise.
        public string SignUp()
        {
            List<string> query = new List<string>();

            // Query for existing entry using Email, store in query

            if (query.Count() == 0)
            {
                // Insert command using local attributes

                return "true";
            }
            return "false";
        }

        // Retrieves challenges completed by user
        public List<string> ChallengesCompleted(string UserID)
        {
            List<string> done = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["Taradisyon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ChallengeID" +
                    "FROM dbo.Collection" +
                    "WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                            done.Add(reader[0]);
                        }
                }
            }
            // Query for challenges done using User ID, store to done

            return done;
        }

        // Code for claiming a challenge token.
        // Requires ChallengeID of challenged to be claimed by user, and challenge token (user input)
        public void ClaimToken(string ChallengeID, string Token)
        {
            ChallengeToken query = new ChallengeToken();

            // Query for TokenID using ChallengeID and Token (user input), if found, set to query.TokenID

            if (query.TokenID != 0)
            {
                // Query ExpiryTime using TokenID, store to query.ExpiryTime

                if (DateTime.Compare(query.ExpiryTime, DateTime.Now) >= 0 && query.isClaimed == false)
                {
                    // Token is valid

                    int Reward;
                    // Query RewardPoints using ChallengeID, store to Reward

                    RewardPoints += Reward;

                    // Update RewardPoints of UserID

                    // Update isClaimed of TokenID to true
                }
            }

        }
    }
}
