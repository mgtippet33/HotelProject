using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
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

        public override bool Equals(object obj)
        {
            if(obj is Reservation)
            {
                var objRes = obj as Reservation;
                return this.ReservationID == objRes.ReservationID &&
                    this.ReservationDate == objRes.ReservationDate &&
                    this.ArrivalDate == objRes.ArrivalDate &&
                    this.DepatureDate == objRes.DepatureDate &&
                    this.SettledIn == objRes.SettledIn &&
                    this.RoomReservation.Equals(objRes.RoomReservation) &&
                    this.ClientResevation.Equals(objRes.ClientResevation);
            }
            return base.Equals(obj);
        }
    }
}
