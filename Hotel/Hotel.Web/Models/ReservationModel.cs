using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hotel.Web.Models
{
    public class ReservationModel
    {
        public int ReservationID { set; get; }
        [DisplayName("Name of room")]
        public RoomModel RoomReservation { set; get; }
        [DisplayName("Client Name")]
        public ClientModel ClientResevation { set; get; }
        [DisplayName("Date of reservation")]
        public DateTime? ReservationDate { set; get; }
        [DisplayName("Date of arrival")]
        public DateTime ArrivalDate { set; get; }
        [DisplayName("Date of departure")]
        public DateTime DepartureDate { set; get; }
        public bool SettledIn { set; get; }
    }
}