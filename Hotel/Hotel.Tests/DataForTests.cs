using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Tests
{
    class DataForTests
    {
        public static List<Category> Categories
        {
            get
            {
                var categoryList = new List<Category>()
                {
                    new Category()
                    {
                        CategoryID = 1,
                        CategoryName = "Single",
                        Capacity = 1
                    },
                    new Category()
                    {
                        CategoryID = 2,
                        CategoryName = "Double",
                        Capacity = 2
                    },
                    new Category()
                    {
                        CategoryID = 3,
                        CategoryName = "Triple",
                        Capacity = 3
                    }
                };
                return categoryList;
            }
        }
        public static List<PriceCategory> PriceCategories
        {
            get
            {
                var priceCategoryList = new List<PriceCategory>()
                {
                    new PriceCategory()
                    {
                        PriceCategoryID = 1,
                        CategoryID = 1,
                        Price = 150,
                        StartDate = DateTime.Parse("01.06.2021"),
                        EndDate = DateTime.Parse("30.08.2021"),
                        CategoryName = Categories[0]
                    },
                    new PriceCategory()
                    {
                        PriceCategoryID = 2,
                        CategoryID = 2,
                        Price = 250,
                        StartDate = DateTime.Parse("01.06.2021"),
                        EndDate = DateTime.Parse("30.08.2021"),
                        CategoryName = Categories[1]

                    },
                    new PriceCategory()
                    {
                        PriceCategoryID = 3,
                        CategoryID = 3,
                        Price = 350,
                        StartDate = DateTime.Parse("01.06.2021"),
                        EndDate = DateTime.Parse("30.08.2021"),
                        CategoryName = Categories[2]
                    }
                };
                return priceCategoryList;
            }
        }
        public static List<Client> Clients
        {
            get
            {
                var clientList = new List<Client>()
                {
                    new Client()
                    {
                        ClientID = 1,
                        Surname = "Ivanov",
                        Name = "Ivan",
                        Passport = "DD 3324214"
                    },
                    new Client()
                    {
                        ClientID = 2,
                        Surname = "Petrov",
                        Name = "Petr",
                        Passport = "BB 3312114"
                    },
                    new Client()
                    {
                        ClientID = 3,
                        Surname = "Semenov",
                        Name = "Semen",
                        Passport = "AA 3314523"
                    }
                };
                return clientList;
            }
        }
        public static List<Room> Rooms
        {
            get
            {
                var roomList = new List<Room>()
                {
                    new Room()
                    {
                        RoomID = 1,
                        RoomName = "100",
                        PriceCategoryID = 1,
                        Active = true,
                        RoomCategory = PriceCategories[0]
                    },
                    new Room()
                    {
                        RoomID = 2,
                        RoomName = "200",
                        PriceCategoryID = 2,
                        Active = true,
                        RoomCategory = PriceCategories[1]
                    },
                    new Room()
                    {
                        RoomID = 3,
                        RoomName = "300",
                        PriceCategoryID = 3,
                        Active = true,
                        RoomCategory = PriceCategories[2]
                    },
                };
                return roomList;
            }
        }
        public static List<Reservation> Reservations
        {
            get
            {
                var reservationList = new List<Reservation>()
                {
                    new Reservation()
                    {
                        ReservationID = 1,
                        RoomID = 1,
                        ClientID = 1,
                        ReservationDate = DateTime.Parse("30.06.2021"),
                        ArrivalDate = DateTime.Parse("01.07.2021"),
                        DepartureDate = DateTime.Parse("15.07.2021"),
                        SettledIn = true,
                        RoomReservation = Rooms[0],
                        ClientResevation = Clients[0]

                    },
                    //new Reservation()
                    //{
                    //    ReservationID = 2,
                    //    RoomID = 1,
                    //    ClientID = 1,
                    //    ReservationDate = DateTime.Parse("30.06.2021"),
                    //    ArrivalDate = DateTime.Parse("01.07.2021"),
                    //    DepatureDate = DateTime.Parse("15.07.2021"),
                    //    SettledIn = true,
                    //    RoomReservation = Rooms[0],
                    //    ClientResevation = Clients[2]
                    //},
                    new Reservation()
                    {
                        ReservationID = 3,
                        RoomID = 2,
                        ClientID = 2,
                        ReservationDate = DateTime.Parse("15.06.2021"),
                        ArrivalDate = DateTime.Parse("04.07.2021"),
                        DepartureDate = DateTime.Parse("10.07.2021"),
                        SettledIn = false,
                        RoomReservation = Rooms[1],
                        ClientResevation = Clients[1]
                    },
                    new Reservation()
                    {
                        ReservationID = 4,
                        RoomID = 3,
                        ClientID = 3,
                        ReservationDate = DateTime.Parse("01.07.2021"),
                        ArrivalDate = DateTime.Parse("15.07.2021"),
                        DepartureDate = DateTime.Parse("25.07.2021"),
                        SettledIn = false,
                        RoomReservation = Rooms[2],
                        ClientResevation = Clients[1]
                    },
                    new Reservation()
                    {
                        ReservationID = 5,
                        RoomID = 3,
                        ClientID = 2,
                        ReservationDate = DateTime.Parse("02.07.2021"),
                        ArrivalDate = DateTime.Parse("02.08.2021"),
                        DepartureDate = DateTime.Parse("21.08.2021"),
                        SettledIn = false,
                        RoomReservation = Rooms[2],
                        ClientResevation = Clients[0]
                    }
                };
                return reservationList;
            }
        }
    }
}
