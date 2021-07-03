using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class ReservationModel
    {
        public int ReservationID { set; get; }
        public RoomModel RoomReservation { set; get; }
        public ClientModel ClientResevation { set; get; }
        public DateTime? ReservationDate { set; get; }
        public DateTime ArrivalDate { set; get; }
        public DateTime DepatureDate { set; get; }
        public bool SettledIn { set; get; }
    }
}