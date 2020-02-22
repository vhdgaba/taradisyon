using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taradisyon
{
    public class Challenge
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int LocationID { get; set; }
        public int AdministratorID { get; set; }
        public string Emblem { get; set; }
        public int Point { get; set; }
        public string Picture { get; set; }
    }
}