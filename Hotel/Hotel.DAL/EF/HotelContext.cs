using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.EF
{
    public class HotelModel : DbContext
    {
        public HotelModel(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<HotelModel>(new HotelInitializer());
        }

        public DbSet<Category> Categories { set; get; }
        public DbSet<PriceCategory> PriceCategories { set; get; }
        public DbSet<Client> Clients { set; get; }
        public DbSet<Room> Rooms { set; get; }
        public DbSet<Reservation> Reservations { set; get; }
    }
}
