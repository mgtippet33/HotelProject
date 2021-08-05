using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public class UserService : IUserService
    {
        private IWorkUnit DataBase { set; get; }
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public UserService(IWorkUnit database)
        {
            this.DataBase = database;
            toDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            fromDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();
        }

        public bool Check(UserDTO item)
        {
            var data = fromDTOMapper.Map<UserDTO, User>(item);
            var result = DataBase.Users.Search(data);
            return result;
        }

        public void Create(UserDTO item)
        {
            var data = fromDTOMapper.Map<UserDTO, User>(item);
            DataBase.Users.Create(data);
            DataBase.Save();
        }

        public void Delete(int id)
        {
            DataBase.Users.Delete(id);
            DataBase.Save();
        }

        public UserDTO Get(int id)
        {
            var user = DataBase.Users.Get(id);
            return toDTOMapper.Map<User, UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return toDTOMapper.Map<IEnumerable<User>, List<UserDTO>>(DataBase.Users.GetAll());
        }

        public void Update(int id, UserDTO item)
        {
            var data = fromDTOMapper.Map<UserDTO, User>(item);
            DataBase.Users.Update(id, data);
            DataBase.Save();
        }

        public UserDTO Login(UserDTO user)
        {
            var data = fromDTOMapper.Map<UserDTO, User>(user);
            var result = DataBase.Users.Login(data);
            return toDTOMapper.Map<User, UserDTO>(result);
        }

        public int GetID(string userName)
        {
            var result = DataBase.Users.GetID(userName);
            return result;
        }
    }
}
