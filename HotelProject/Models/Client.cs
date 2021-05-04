using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    class Client
    {
        private int clientID;
        private string surname;
        private string name;
        private string middleName;
        private DateTime birthday;
        private string address;

        public int ClientID
        {
            get { return clientID; }
            private set { clientID = value; }
        }

        public string Surname
        {
            get { return surname; }
            private set { surname = value; }
        }

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            private set { middleName = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            private set { birthday = value; }
        }

        public string Address
        {
            get { return address; }
            private set { address = value; }
        }


        public Client(int clientID, string surname, string name, string middleName, DateTime birthday, string address)
        {
            this.clientID = clientID;
            this.surname = surname;
            this.name = name;
            this.middleName = middleName;
            this.birthday = birthday;
            this.address = address;
        }
    }
}
