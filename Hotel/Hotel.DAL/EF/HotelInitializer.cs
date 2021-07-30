using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.EF
{
    public class HotelInitializer : DropCreateDatabaseAlways<HotelModel>
    {
        private void CategoryInitializer(HotelModel context)
        {
            var categoryList = new List<Category>()
            {
                new Category()
                {
                    CategoryName = "Single",
                    Capacity = 1
                },
                new Category()
                {
                    CategoryName = "Double",
                    Capacity = 2
                },
                new Category()
                {
                    CategoryName = "Triple",
                    Capacity = 3
                }
            };
            foreach (var category in categoryList)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        private void PriceCategoryInitializer(HotelModel context)
        {
            var priceCategoryList = new List<PriceCategory>()
            {
                new PriceCategory()
                {
                    CategoryID = 1,
                    Price = 150,
                    StartDate = DateTime.Parse("01.06.2021"),
                    EndDate = DateTime.Parse("30.08.2021")
                },
                new PriceCategory()
                {
                    CategoryID = 2,
                    Price = 250,
                    StartDate = DateTime.Parse("01.06.2021"),
                    EndDate = DateTime.Parse("30.08.2021")
                },
                new PriceCategory()
                {
                    CategoryID = 3,
                    Price = 350,
                    StartDate = DateTime.Parse("01.06.2021"),
                    EndDate = DateTime.Parse("30.08.2021")
                }
            };
            foreach (var priceCategory in priceCategoryList)
            {
                context.PriceCategories.Add(priceCategory);
            }
            context.SaveChanges();
        }
        private void ClientInitializer(HotelModel context)
        {
            var clientList = new List<Client>()
            {
                new Client()
                {
                    Surname = "Ivanov",
                    Name = "Ivan",
                    Passport = "DD 3324214"
                },
                new Client()
                {
                    Surname = "Petrov",
                    Name = "Petr",
                    Passport = "BB 3312114"
                },
                new Client()
                {
                    Surname = "Semenov",
                    Name = "Semen",
                    Passport = "AA 3314523"
                }
            };
            foreach (var client in clientList)
            {
                context.Clients.Add(client);
            }
            context.SaveChanges();
        }

        private void UserInitializer(HotelModel context)
        {
            var userList = new List<User>()
            {
                new User()
                {
                    Login = "nix",
                    Password = "nix",
                    Surname = "nix",
                    Name = "nix"
                }
            };
            foreach (var user in userList)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }

        private void RoomInitializer(HotelModel context)
        {
            var roomList = new List<Room>()
            {
                new Room()
                {
                    RoomName = "100",
                    PriceCategoryID = 1,
                    Active = true
                },
                new Room()
                {
                    RoomName = "200",
                    PriceCategoryID = 2,
                    Active = true
                },
                new Room()
                {
                    RoomName = "300",
                    PriceCategoryID = 3,
                    Active = true
                },
            };
            foreach (var room in roomList)
            {
                context.Rooms.Add(room);
            }
            context.SaveChanges();
        }
        private void ReservationInitializer(HotelModel context)
        {
            var reservationList = new List<Reservation>()
            {
                new Reservation()
                {
                    RoomID = 1,
                    ClientID = 1,
                    UserID = 1,
                    ReservationDate = DateTime.Parse("30.06.2021"),
                    ArrivalDate = DateTime.Parse("01.07.2021"),
                    DepatureDate = DateTime.Parse("15.07.2021"),
                    SettledIn = true
                },
                new Reservation()
                {
                    RoomID = 1,
                    ClientID = 1,
                    UserID = 1,
                    ReservationDate = DateTime.Parse("30.06.2021"),
                    ArrivalDate = DateTime.Parse("01.07.2021"),
                    DepatureDate = DateTime.Parse("15.07.2021"),
                    SettledIn = true
                },
                new Reservation()
                {
                    RoomID = 2,
                    ClientID = 2,
                    UserID = 1,
                    ReservationDate = DateTime.Parse("15.06.2021"),
                    ArrivalDate = DateTime.Parse("04.07.2021"),
                    DepatureDate = DateTime.Parse("10.07.2021"),
                    SettledIn = false
                },
                new Reservation()
                {
                    RoomID = 3,
                    ClientID = 3,
                    UserID = 1,
                    ReservationDate = DateTime.Parse("01.07.2021"),
                    ArrivalDate = DateTime.Parse("10.07.2021"),
                    DepatureDate = DateTime.Parse("15.07.2021"),
                    SettledIn = false
                },
                new Reservation()
                {
                    RoomID = 3,
                    ClientID = 2,
                    UserID = 1,
                    ReservationDate = DateTime.Parse("02.07.2021"),
                    ArrivalDate = DateTime.Parse("02.08.2021"),
                    DepatureDate = DateTime.Parse("21.08.2021"),
                    SettledIn = false
                }
            };
            foreach (var reservation in reservationList)
            {
                context.Reservations.Add(reservation);
            }
            context.SaveChanges();
        }

        protected override void Seed(HotelModel context)
        {
            CategoryInitializer(context);
            PriceCategoryInitializer(context);
            ClientInitializer(context);
            UserInitializer(context);
            RoomInitializer(context);
            ReservationInitializer(context);
        }
    }
}
