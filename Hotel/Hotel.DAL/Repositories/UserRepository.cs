using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    class UserRepository : IRepository<User>
    {
        private HotelModel db;

        public UserRepository(HotelModel db)
        {
            this.db = db;
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = Get(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public bool Search(User item)
        {
            if (item.Login == null || item.Surname == null || item.Name == null)
                return false;
            User data = db.Users.SingleOrDefault(st => st.Login == item.Login);
            if (data != null)
                return true;
            return false;
        }

        public void Update(int id, User item)
        {
            var userUpdate = db.Users.FirstOrDefault(m => m.UserID == id);
            if (userUpdate != null)
            {
                userUpdate.UserID = id;
                userUpdate.Surname = item.Surname ?? userUpdate.Surname;
                userUpdate.Name = item.Name ?? userUpdate.Name;
                userUpdate.Login = item.Login ?? userUpdate.Login;
                userUpdate.Password = item.Password ?? userUpdate.Password;
            }
        }
    }
}
