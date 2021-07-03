using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class ClientModel
    {
        public int ClientID { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }
        public string Passport { set; get; }
    }
}