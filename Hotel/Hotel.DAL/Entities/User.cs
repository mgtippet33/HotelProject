using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class User
    {
        [Key]
        public int UserID { set; get; }
        [Required]
        public string Login { set; get; }
        [Required]
        public string Password { set; get; }
        [Required]
        public string Surname { set; get; }
        [Required]
        public string Name { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                var objSt = obj as User;
                return this.UserID == objSt.UserID && this.Surname == objSt.Login &&
                    this.Password == objSt.Password && this.Surname == objSt.Surname &&
                    this.Name == objSt.Name;
            }
            return base.Equals(obj);
        }
    }
}
