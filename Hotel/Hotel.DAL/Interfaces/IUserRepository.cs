using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        int GetID(string userName);
        void Create(User item);
        void Update(int id, User item);
        void Delete(int id);
        bool Search(User item);
        User Login(User user);
    }
}
