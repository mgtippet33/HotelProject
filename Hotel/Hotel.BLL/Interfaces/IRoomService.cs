using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetAllRooms();
        RoomDTO Get(int roomID);
        void Create(RoomDTO room);
        void Update(int roomID, RoomDTO room);
        void Delete(int roomID);
        bool CheckRoom(RoomDTO room);
    }
}
