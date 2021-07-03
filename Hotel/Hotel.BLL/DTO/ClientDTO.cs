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
    }
}
