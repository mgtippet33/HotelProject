using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Web.Models
{
    public class UserModel
    {
        public int UserID { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is UserModel)
            {
                var objModel = obj as UserModel;
                return this.UserID == objModel.UserID && this.Surname == objModel.Login &&
                    this.Password == objModel.Password && this.Surname == objModel.Surname &&
                    this.Name == objModel.Name;
            }
            return base.Equals(obj);
        }
    }
}