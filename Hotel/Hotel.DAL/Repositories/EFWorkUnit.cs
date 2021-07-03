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
    public class EFWorkUnit : IWorkUnit
    {
        private HotelModel db;
        private ClientRepository clientRepository;
        private CategoryRepository categoryRepository;
        private PriceCategoryRepository priceCategoryRepository;
        private RoomRepository roomRepository;
        private ReservationRepository reservationRepository;

        public EFWorkUnit(string connectionString)
        {
            db = new HotelModel(connectionString);
        }

        public IRepository<Client> Clients
        {
            get
            {
                if(clientRepository == null)
                {
                    clientRepository = new ClientRepository(db);
                }
                return clientRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }
                return categoryRepository;
            }
        }

        public IRepository<PriceCategory> PriceCategories
        {
            get
            {
                if (priceCategoryRepository == null)
                {
                    priceCategoryRepository = new PriceCategoryRepository(db);
                }
                return priceCategoryRepository;
            }
        }

        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(db);
                }
                return roomRepository;
            }
        }

        public IRepository<Reservation> Reservations
        {
            get
            {
                if (reservationRepository == null)
                {
                    reservationRepository = new ReservationRepository(db);
                }
                return reservationRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
