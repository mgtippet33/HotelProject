using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(int id);
        int GetID(string userName);
        void Create(UserDTO item);
        void Update(int id, UserDTO item);
        void Delete(int id);
        bool Check(UserDTO item);
        UserDTO Login(UserDTO item);
    }
}
