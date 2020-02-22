// Classes for Account related functions

using System;
using System.Collections.Generic;

namespace Account
{
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
        public bool Login()
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

                        return true;
                    }
                }
            }
            return false;
        }

        // Code for signing up.
        // Returns true for successful account creation, false otherwise
        public bool SignUp()
        {
            List<string> query = new List<string>();

            // Query for existing entry using Email, store in query

            if (query.Count() == 0)
            {
                // Insert command using local attributes

                return true;
            }
            return false;
        }
    }
}