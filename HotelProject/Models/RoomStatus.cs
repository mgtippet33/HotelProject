using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    class RoomStatus
    {
        private int roomNumber;
        private bool isFree;
        private DateTime? entryDate;
        private DateTime? exitDate;
        private bool settle;
        private int? clientID;

        public int RoomNumber
        {
            get { return roomNumber; }
            private set { roomNumber = value; }
        }

        public bool IsFree
        {
            get { return isFree; }
            private set { isFree = value; }
        }

        public DateTime? EntryDate
        {
            get { return entryDate; }
            private set { entryDate = value; }
        }

        public DateTime? ExitDate
        {
            get { return exitDate; }
            private set { exitDate = value; }
        }

        public bool Settle
        {
            get { return settle; }
            private set { settle = value; }
        }

        public int? ClientID
        {
            get { return clientID; }
            private set { clientID = value; }
        }

        public RoomStatus(int roomNumber, bool isFree, DateTime? entryDate, DateTime? exitDate, bool settle, int? clientID)
        {
            this.roomNumber = roomNumber;
            this.isFree = isFree;
            this.entryDate = entryDate;
            this.exitDate = exitDate;
            this.settle = settle;
            this.clientID = clientID;
        }

        public void Edit(bool isFree, DateTime? entryDate, DateTime? exitDate, int? clientID)
        {
            this.isFree = isFree;
            this.entryDate = entryDate;
            this.exitDate = exitDate;
            this.clientID = clientID;
        }

        public void DroveIn(bool settle)
        {
            this.settle = settle;
        }
    }
}
