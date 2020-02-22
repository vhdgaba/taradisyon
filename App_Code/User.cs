// Classes for Account related functions

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taradisyon
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public int Point { get; set; }
    }
}