using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { set; get; }
        public int RoomID { set; get; }
        public int ClientID { set; get; }
        public DateTime? ReservationDate { set; get; }
        public DateTime ArrivalDate { set; get; }
        public DateTime DepatureDate { set; get; }
        public bool SettledIn { set; get; }


        [ForeignKey("RoomID")]
        public virtual Room RoomReservation { set; get; }
        [ForeignKey("ClientID")]
        public virtual Client ClientResevation { set; get; }
    }
}