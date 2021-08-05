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
    class RoomRepository : IRepository<Room>
    {
        private HotelModel db;

        public RoomRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms;
        }

        public Room Get(int roomID)
        {
            return db.Rooms.Find(roomID);
        }

        public void Create(Room room)
        {
            room.RoomCategory = db.PriceCategories.Find(room.RoomCategory.PriceCategoryID);
            db.Rooms.Add(room);
        }

        public void Delete(int roomID)
        {
            Room room = Get(roomID);
            if (room != null)
            {
                db.Rooms.Remove(room);
            }
        }

        public void Update(int roomID, Room value)
        {
            var roomUpdate = db.Rooms.FirstOrDefault(m => m.RoomID == roomID);
            if (roomUpdate != null)
            {
                roomUpdate.RoomCategory = db.PriceCategories.Find(value.RoomCategory.PriceCategoryID) ?? roomUpdate.RoomCategory;
                roomUpdate.RoomID = roomID;
                roomUpdate.RoomName = value.RoomName ?? roomUpdate.RoomName;
                roomUpdate.Active = value.Active;
            }
        }

        public bool Search(Room room)
        {
            Room data = db.Rooms.SingleOrDefault(item => item.RoomName == room.RoomName);
            if (data != null)
                return true;
            return false;
        }
    }
}
