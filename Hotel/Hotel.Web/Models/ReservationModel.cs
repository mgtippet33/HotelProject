using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("User Name")]
        public UserModel UserResevation { set; get; }
        [DisplayName("Date of reservation")]
        public DateTime? ReservationDate { set; get; }
        [DisplayName("Date of arrival")]
        public DateTime ArrivalDate { set; get; }
        [DisplayName("Date of departure")]
        public DateTime DepartureDate { set; get; }
        [DisplayName("Settled In")]
        public bool SettledIn { set; get; }

        public string ActionUserName { get; set; }
        public string ActionType { get; set; }
        public DateTime ActionTime { get; set; }
    }
}