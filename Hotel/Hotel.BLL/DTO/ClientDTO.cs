using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class ClientDTO
    {
        public int ClientID { set; get; }
        public string Surname { set; get; }
        public string Name { set; get; }
        public string Passport { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is ClientDTO)
            {
                var objDTO = obj as ClientDTO;
                return this.ClientID == objDTO.ClientID && this.Surname == objDTO.Surname &&
                    this.Name == objDTO.Name && this.Passport == objDTO.Passport;
            }
            return base.Equals(obj);
        }
    }
}
