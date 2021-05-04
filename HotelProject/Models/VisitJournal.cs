using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelProject.Models
{
    class VisitJournal
    {
        private List<RoomStatus> journal;
        private Rooms rooms;
        private Clients clients;

        public VisitJournal()
        {
            journal = new List<RoomStatus>();
            rooms = new Rooms();
            clients = new Clients();
            FillJournal();
        }

        public void AddRoom(int roomNumber, int numberOfSeats, string category, int price)
        {
            var index = journal.Select(room => room.RoomNumber).ToList().IndexOf(roomNumber);
            if (index == -1)
            {
                rooms.Add(roomNumber, numberOfSeats, category, price);
                RoomStatus roomStatus = new RoomStatus(roomNumber, true, null, null, false, null);
                journal.Add(roomStatus);
            }
            else
            {
                Console.WriteLine("Данный номер уже существует!");
            }
        }

        public void RemoveRoom(int roomNumber)
        {
            var index = journal.Select(item => item.RoomNumber).ToList().IndexOf(roomNumber);
            if(index != -1)
            {
                rooms.Remove(roomNumber);
                journal.RemoveAt(index);
            }

        }

        public int AddClient(string surname, string name, string middleName, DateTime birthday, string address)
        {
            return clients.Add(surname, name, middleName, birthday, address);

        }

        public void RemoveClient(int clientID)
        {
            clients.Remove(clientID);
        }

        public void BookRoom(int roomNumber, int clientID, DateTime from, DateTime to)
        {
            var index = journal.Select(room => room.RoomNumber).ToList().IndexOf(roomNumber);
            if(index != -1)
            {
                if (journal[index].IsFree)
                {
                    journal[index].Edit(false, from, to, clientID);
                }
            }
        }

        public void CheckInClient(int clientID, int roomNumber = -1, DateTime? from = null, DateTime? to = null)
        {
            var clientIndex = journal.Select(room => room.ClientID).ToList().IndexOf(clientID);
            var roomIndex = journal.Select(room => room.RoomNumber).ToList().IndexOf(roomNumber);
            if (clientIndex != -1)
            {
                journal[clientIndex].DroveIn(true);
            }
            else if(roomIndex != -1)
            {
                if (journal[roomIndex].IsFree)
                {
                    journal[roomIndex].Edit(false, from.Value, to.Value, clientID);
                    journal[roomIndex].DroveIn(true);
                }
            }
        }

        public void CheckOutClient(int roomNumber)
        {
            var roomIndex = journal.Select(room => room.RoomNumber).ToList().IndexOf(roomNumber);
            if(roomIndex != -1)
            {
                journal[roomIndex].Edit(true, null, null, null);
                journal[roomIndex].DroveIn(false);
            }

        }

        public int FreeRoomsCount(DateTime fromDate, DateTime toDate)
        {
            var freeRooms = journal.Where(room => (room.EntryDate == null && room.ExitDate == null) || 
                                          (room.EntryDate.Value > fromDate && room.ExitDate.Value < toDate) ||
                                          (room.ExitDate.Value < fromDate && room.ExitDate.Value > toDate) ||
                                          (room.ExitDate.Value < fromDate && room.ExitDate.Value < toDate)).Count();
            return freeRooms;

        }

        private void FillJournal()
        {
            try
            {
                using (var file = File.Open(@"journal.txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (var stream = new StreamReader(file))
                    {
                        int count = Int32.Parse(stream.ReadLine());
                        for (int i = 0; i < count; ++i)
                        {
                            string[] item = stream.ReadLine().Split();
                            DateTime? entryDate = null;
                            DateTime? exitDate = null;
                            int? clientID = null;
                            if (item[2] != "null")
                            {
                                entryDate = DateTime.Parse(item[2]);
                            }
                            if (item[3] != "null")
                            {
                                exitDate = DateTime.Parse(item[3]);
                            }
                            if (item[5] != "null")
                            {
                                clientID = Int32.Parse(item[5]);
                            }
                            RoomStatus roomStatus = new RoomStatus(Int32.Parse(item[0]), Boolean.Parse(item[1]), entryDate,
                                exitDate, Boolean.Parse(item[4]), clientID);
                            journal.Add(roomStatus);
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void SaveJournal()
        {
            try
            {
                using (var file = File.Open(@"journal.txt", FileMode.Create, FileAccess.Write))
                {
                    using (var stream = new StreamWriter(file))
                    {
                        int count = journal.Count;
                        stream.WriteLine(count);
                        foreach (RoomStatus status in journal)
                        {
                            string entryDate = status.EntryDate == null ? "null" : status.EntryDate.Value.ToShortDateString();
                            string exitDate = status.ExitDate == null ? "null" : status.ExitDate.Value.ToShortDateString();
                            string clientID = status.ClientID == null ? "null" : status.ClientID.ToString();
                            stream.WriteLine($"{status.RoomNumber} {status.IsFree} {entryDate} {exitDate} {status.Settle} {clientID}");
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        public void Save()
        {
            SaveJournal();
            rooms.SaveRooms();
            clients.SaveClients();
        }
    }
}
