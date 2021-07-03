using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IClientService
    {
        IEnumerable<ClientDTO> GetAllClients();
        ClientDTO Get(int clientID);
        void Create(ClientDTO client);
        void Update(int clientID, ClientDTO client);
        void Delete(int clientID);
        bool CheckClient(ClientDTO client);                
    }
}
