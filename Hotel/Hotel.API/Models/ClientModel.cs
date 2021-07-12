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

        public override bool Equals(object obj)
        {
            if(obj is ClientModel)
            {
                var objCM = obj as ClientModel;
                return this.ClientID == objCM.ClientID && this.Surname == objCM.Surname &&
                    this.Name == objCM.Name && this.Passport == objCM.Passport;       
            }
            return base.Equals(obj);
        }
    }
}