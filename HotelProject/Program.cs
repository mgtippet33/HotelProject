using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HotelProject.Models;

namespace HotelProject
{
    class Program
    {
        static void Main(string[] args)
        {
            VisitJournal journal = new VisitJournal();
            Console.WriteLine("Укажите пункт, который хотите выполнить:");
            while (true)
            {
                Console.WriteLine("1 - Добавить номер");
                Console.WriteLine("2 - Удалить номер");
                Console.WriteLine("3 - Забронировать номер");
                Console.WriteLine("4 - Кол-во свободных номеров");
                Console.WriteLine("5 - Регистрация клиента");
                Console.WriteLine("6 - Выезд клиента");
                Console.WriteLine("0 - Выход");
                int num = Int32.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        Console.WriteLine("Номер комнаты:");
                        int roomNumber = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Кол-во комнат:");
                        int numberOfSeats = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Категория:");
                        string category = Console.ReadLine();
                        Console.WriteLine("Цена:");
                        int price = Int32.Parse(Console.ReadLine());
                        journal.AddRoom(roomNumber, numberOfSeats, category, price);
                        break;
                    case 2:
                        Console.WriteLine("Номер комнаты:");
                        roomNumber = Int32.Parse(Console.ReadLine());
                        journal.RemoveRoom(roomNumber);
                        break;
                    case 3:
                        int clientID = AddClient(ref journal);
                        if(clientID == -1)
                        {
                            break;
                        }
                        Console.WriteLine("Номер комнаты:");
                        roomNumber = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Дата заезда:");
                        DateTime from, to;
                        bool check = DateTime.TryParse(Console.ReadLine(), out from);
                        if (!check)
                        {
                            Console.WriteLine("Неккоректный формат даты заезда!");
                            break;
                        }
                        Console.WriteLine("Дата выезда:");
                        check = DateTime.TryParse(Console.ReadLine(), out to);
                        if (!check)
                        {
                            Console.WriteLine("Неккоректный формат даты выезда!");
                            break;
                        }
                        journal.BookRoom(roomNumber, clientID, from, to);
                        break;
                    case 4:
                        Console.WriteLine("Поиск свободных номеров");
                        Console.WriteLine("Дата заезда:");
                        check = DateTime.TryParse(Console.ReadLine(), out from);
                        if (!check)
                        {
                            Console.WriteLine("Неккоректный формат даты заезда!");
                            break;
                        }
                        Console.WriteLine("Дата выезда:");
                        check = DateTime.TryParse(Console.ReadLine(), out to);
                        if (!check)
                        {
                            Console.WriteLine("Неккоректный формат даты выезда!");
                            break;
                        }
                        int freeRooms = journal.FreeRoomsCount(from, to);
                        Console.WriteLine($"Кол-во свободных номеров - {freeRooms}");
                        break;
                    case 5:
                        clientID = AddClient(ref journal);
                        Console.WriteLine("Бронировали? Y\\N");
                        string ans = Console.ReadLine();
                        if(ans.ToLower() == "y")
                        {
                            string res = journal.CheckInClient(clientID) ? "Вы успешно заселились в отель" : "Вы не бронировали номер";
                            Console.WriteLine(res);
                        }
                        else if(ans.ToLower() == "n")
                        {
                            RegistrationRoom(ref journal, clientID);
                        }
                        break;
                    case 6:
                        Console.WriteLine("Номер комнаты:");
                        roomNumber = Int32.Parse(Console.ReadLine());
                        journal.CheckOutClient(roomNumber);
                        break;
                    case 0:
                        journal.Save();
                        return;
                    default:
                        Console.WriteLine("Укажите один из предложенных пунктов");
                        break;
                }
                Console.WriteLine("--------------------");
            }
        }

        public static int AddClient(ref VisitJournal journal)
        {
            Console.WriteLine("Информация о клиенте:");
            Console.WriteLine("Фамилия:");
            string surname = Console.ReadLine();
            Console.WriteLine("Имя:");
            string name = Console.ReadLine();
            Console.WriteLine("Отчество:");
            string middleName = Console.ReadLine();
            Console.WriteLine("Дата Рождения:");
            DateTime birthday;
            bool check = DateTime.TryParse(Console.ReadLine(), out birthday);
            if (!check)
            {
                Console.WriteLine("Неккоректный формат даты!");
                return -1;
            }
            Console.WriteLine("Адрес:");
            string address = Console.ReadLine();
            int clientID = journal.AddClient(surname, name, middleName, birthday, address);
            return clientID;
        }

        public static void RegistrationRoom(ref VisitJournal journal, int clientID)
        {
            Console.WriteLine("Номер комнаты:");
            int roomNumber = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Дата заезда:");
            DateTime from, to;
            bool check = DateTime.TryParse(Console.ReadLine(), out from);
            if (!check)
            {
                Console.WriteLine("Неккоректный формат даты заезда!");
                return;
            }
            Console.WriteLine("Дата выезда:");
            check = DateTime.TryParse(Console.ReadLine(), out to);
            if (!check)
            {
                Console.WriteLine("Неккоректный формат даты выезда!");
                return;
            }
            string res = journal.CheckInClient(clientID) ? "Вы успешно заселились в отель" : "Данный номер занят";
            Console.WriteLine(res);
        }
    }
}
