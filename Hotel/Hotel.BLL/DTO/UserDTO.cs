using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class UserDTO
    {
        public int UserID { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is UserDTO)
            {
                var objDTO = obj as UserDTO;
                return this.UserID == objDTO.UserID && this.Surname == objDTO.Login &&
                    this.Password == objDTO.Password && this.Surname == objDTO.Surname &&
                    this.Name == objDTO.Name;
            }
            return base.Equals(obj);
        }
    }
}
