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
    class ClientRepository : IRepository<Client>
    {
        private HotelModel db;

        public ClientRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients;
        }

        public Client Get(int clientID)
        {
            return db.Clients.Find(clientID);
        }

        public void Create(Client client)
        {
            db.Clients.Add(client);
        }

        public void Update(int clientID, Client value)
        {
            var clientUpdate = db.Clients.FirstOrDefault(m => m.ClientID == clientID);
            if (clientUpdate != null)
            {
                clientUpdate.ClientID = clientID;
                clientUpdate.Surname = value.Surname ?? clientUpdate.Surname;
                clientUpdate.Name = value.Name ?? clientUpdate.Name;
                clientUpdate.Passport = value.Passport ?? clientUpdate.Passport;
            }
        }

        public void Delete(int clientID)
        {
            Client client = Get(clientID);
            if(client != null)
            {
                db.Clients.Remove(client);
            }
        }

        public bool Search(Client client)
        {
            if (client.Passport == null || client.Surname == null || client.Name == null)
                return false;
            Client data = db.Clients.SingleOrDefault(cl => cl.Passport == client.Passport);
            if (data != null)
                return true;
            return false;
        }
    }
}
