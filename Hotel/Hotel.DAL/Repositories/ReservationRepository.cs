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
    class ReservationRepository : IRepository<Reservation>
    {
        private HotelModel db;

        public ReservationRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return db.Reservations;
        }

        public Reservation Get(int reservationID)
        {
            return db.Reservations.Find(reservationID);
        }

        public void Create(Reservation reservation)
        {
            reservation.ClientResevation = db.Clients.Find(reservation.ClientResevation.ClientID);
            reservation.RoomReservation = db.Rooms.Find(reservation.RoomReservation.RoomID);
            reservation.ReservationDate = DateTime.Now;
            db.Reservations.Add(reservation);
        }

        public void Delete(int reservationID)
        {
            Reservation reservation = Get(reservationID);
            if (reservation != null)
            {
                db.Reservations.Remove(reservation);
            }
        }

        public void Update(int reservationID, Reservation value)
        {
            var reservationUpdate = db.Reservations.FirstOrDefault(m => m.ReservationID == reservationID);
            if (reservationUpdate != null)
            {
                reservationUpdate.ClientResevation = db.Clients.Find(value.ClientResevation.ClientID) ?? reservationUpdate.ClientResevation;
                reservationUpdate.RoomReservation = db.Rooms.Find(value.RoomReservation.RoomID) ?? reservationUpdate.RoomReservation;
                reservationUpdate.ReservationID = reservationID;
                reservationUpdate.ReservationDate = value.ReservationDate ?? reservationUpdate.ReservationDate;
                reservationUpdate.ArrivalDate = value.ArrivalDate != reservationUpdate.ArrivalDate ? value.ArrivalDate : reservationUpdate.ArrivalDate;
                reservationUpdate.DepatureDate = value.DepatureDate != reservationUpdate.DepatureDate ? value.DepatureDate : reservationUpdate.DepatureDate;
                reservationUpdate.SettledIn = value.SettledIn;
            }
        }
        public bool Search(Reservation reservation)
        {
            var data = db.Reservations.SingleOrDefault(item => item.RoomReservation.RoomID == reservation.RoomReservation.RoomID &&
            item.ArrivalDate == reservation.ArrivalDate && item.DepatureDate == reservation.DepatureDate);
            if (data != null)
                return true;
            return false;
        }
    }
}
