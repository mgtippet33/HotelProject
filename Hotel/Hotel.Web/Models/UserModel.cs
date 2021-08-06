using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace Hotel.Web.Models
{
    public class UserModel
    {
        public int UserID { set; get; }
        [Display(Name = "Username")]
        [Required]
        public string Login { set; get; }
        [DataType(DataType.Password)]
        [Required]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6}$")]
        public string Password { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }

        public string HashedPassword
        {
            get
            {
                return Crypto.Hash(this.Password);
            }
        }

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