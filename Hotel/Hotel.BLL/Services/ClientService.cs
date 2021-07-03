using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class ClientService : IService<ClientDTO>
    {
        private IWorkUnit DataBase { get; set; }
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public ClientService(IWorkUnit database)
        {
            this.DataBase = database;
            toDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            fromDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
        }

        public IEnumerable<ClientDTO> GetAll()
        {
            return toDTOMapper.Map<IEnumerable<Client>, List<ClientDTO>>(DataBase.Clients.GetAll());
        }

        public ClientDTO Get(int clientID)
        {
            var client = DataBase.Clients.Get(clientID);
            return toDTOMapper.Map<Client, ClientDTO>(client);
        }

        public void Create(ClientDTO client)
        {
            var data = fromDTOMapper.Map<ClientDTO, Client>(client);
            DataBase.Clients.Create(data);
            DataBase.Save();
        }
        
        public void Update(int clientID, ClientDTO client)
        {
            var data = fromDTOMapper.Map<ClientDTO, Client>(client);
            DataBase.Clients.Update(clientID, data);
            DataBase.Save();
        }

        public void Delete(int clientID)
        {
            DataBase.Clients.Delete(clientID);
            DataBase.Save();
        }

        public bool Check(ClientDTO client)
        {
            var data = fromDTOMapper.Map<ClientDTO, Client>(client);
            var result = DataBase.Clients.Search(data);
            return result;
        }
    }
}
