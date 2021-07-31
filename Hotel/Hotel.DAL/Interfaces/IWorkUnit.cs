using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Interfaces
{
    public interface IWorkUnit
    {
        IRepository<Client> Clients { get; }
        IUserRepository Users { get; }
        IRepository<Category> Categories { get; }
        IRepository<PriceCategory> PriceCategories { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Reservation> Reservations { get; }
        void Save();
    }
}
