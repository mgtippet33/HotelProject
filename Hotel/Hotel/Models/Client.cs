using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Client
    {
        [Key]        
        public int ClientID { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }
        public string Passport { set; get; }
    }
}