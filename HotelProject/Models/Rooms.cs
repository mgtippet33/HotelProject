using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelProject.Models
{
    class Rooms
    {
        private Dictionary<int, Room> rooms;

        public Rooms()
        {
            rooms = new Dictionary<int, Room>();
            FillRooms();
        }

        public void Add(int roomNumber, int numberOfSeats, string category, int price)
        {
            if (!rooms.ContainsKey(roomNumber))
            {
                Room newRoom = new Room(roomNumber, numberOfSeats, category, price);
                rooms.Add(roomNumber, newRoom);
            }
        }

        public void Remove(int roomNumber)
        {
            if (rooms.ContainsKey(roomNumber))
            {
                rooms.Remove(roomNumber);
            }
        }

        private void FillRooms()
        {
            try
            {
                using (var file = File.Open(@"rooms.txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (var stream = new StreamReader(file))
                    {
                        int count = Int32.Parse(stream.ReadLine());
                        for (int i = 0; i < count; ++i)
                        {
                            string[] room = stream.ReadLine().Split();
                            int roomNumber = Int32.Parse(room[0]);
                            rooms.Add(roomNumber, new Room(roomNumber, Int32.Parse(room[1]), room[2],
                                Int32.Parse(room[3])));
                        }
                    }
                }
            }
            catch(Exception) { }
        }

        public void SaveRooms()
        {
            try
            {
                using (var file = File.Open(@"rooms.txt", FileMode.Create, FileAccess.Write))
                {
                    using (var stream = new StreamWriter(file))
                    {
                        int count = rooms.Count;
                        stream.WriteLine(count);
                        foreach (KeyValuePair<int, Room> item in rooms)
                        {
                            Room room = item.Value;
                            stream.WriteLine($"{room.RoomNumber} {room.NumberOfSeats} {room.Category} {room.Price}");
                        }
                    }
                }
            }
            catch(Exception) { }
        }
    }
}
