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
    public class RegisterUserModel
    {
        [Required]
        public string Login { set; get; }
        [DataType(DataType.Password)]
        [Required]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6}$")]
        public string Password { set; get; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords didn't match")]
        [Required]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6}$")]
        public string RepeatPassword { set; get; }
        [Required]
        public string Surname { set; get; }
        [Required]
        public string Name { set; get; }

        public string HashedPassword
        {
            get
            {
                return Crypto.Hash(this.Password);
            }
        }
    }
}