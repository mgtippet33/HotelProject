using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    class Room
    {
        private int roomNumber;
        private int numberOfSeats;
        private string category;
        private int price;

        public int RoomNumber
        {
            get { return roomNumber; }
            private set { roomNumber = value; }
        }

        public int NumberOfSeats
        {
            get { return numberOfSeats; }
            private set { numberOfSeats = value; }
        }

        public string Category
        {
            get { return category; }
            private set { category = value; }
        }

        public int Price
        {
            get { return price; }
            private set { price = value; }
        }


        public Room(int roomNumber, int numberOfSeats, string category, int price)
        {
            this.roomNumber = roomNumber;
            this.numberOfSeats = numberOfSeats;
            this.category = category;
            this.price = price;
        }
    }
}
