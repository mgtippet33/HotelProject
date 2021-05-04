using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HotelProject.Models
{
    class Clients
    {
        private Dictionary<int, Client> clients;
        private Random rand = new Random();

        public Clients()
        {
            clients = new Dictionary<int, Client>();
            FillClients();
        }

        public int Add(string surname, string name, string middleName, DateTime birthday, string address)
        {
            var cl = clients.Select(client => client.Value).Where(client => client.Surname == surname && client.Name == name &&
                                                                  client.MiddleName == middleName && client.Birthday == birthday.Date &&
                                                                  client.Address == address).ToList();
            if(cl.Count == 0)
            {
                int clientID = rand.Next(0, 1000);
                clients.Add(clientID, new Client(clientID, surname, name, middleName, birthday, address));
                return clientID;
            }
            return cl[0].ClientID;
            
            
        }

        public void Remove(int clientID)
        {
            if(clients.ContainsKey(clientID))
            {
                clients.Remove(clientID);
            }
        }

        private void FillClients()
        {
            try
            {
                using (var file = File.Open(@"clients.txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (var stream = new StreamReader(file))
                    {
                        int count = Int32.Parse(stream.ReadLine());
                        for (int i = 0; i < count; ++i)
                        {
                            string[] client = stream.ReadLine().Split();
                            int clientID = Int32.Parse(client[0]);
                            clients.Add(clientID, new Client(clientID, client[1], client[2], client[3],
                                DateTime.Parse(client[4]), client[5]));
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        public void SaveClients()
        {
            try
            {
                using (var file = File.Open(@"clients.txt", FileMode.Create, FileAccess.Write))
                {
                    using (var stream = new StreamWriter(file))
                    {
                        int count = clients.Count;
                        stream.WriteLine(count);
                        foreach (KeyValuePair<int, Client> item in clients)
                        {
                            Client client = item.Value;
                            stream.WriteLine($"{client.ClientID} {client.Surname} {client.Name} {client.MiddleName} " +
                                $"{client.Birthday.ToShortDateString()} {client.Address}");
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
