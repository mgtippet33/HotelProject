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
        [Required]
        public int RoomID { set; get; }
        [Required]
        public int ClientID { set; get; }
        //public int UserID { set; get; }
        [Column(TypeName = "datetime2")]
        public DateTime? ReservationDate { set; get; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ArrivalDate { set; get; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime DepartureDate { set; get; }
        [Required]
        public bool SettledIn { set; get; }


        [ForeignKey("RoomID")]
        [Required]
        public virtual Room RoomReservation { set; get; }
        [ForeignKey("ClientID")]
        [Required]
        public virtual Client ClientResevation { set; get; }
        //[ForeignKey("UserID")]
        //public virtual User UserReservation { set; get; }

        public override bool Equals(object obj)
        {
            if(obj is Reservation)
            {
                var objRes = obj as Reservation;
                return this.ReservationID == objRes.ReservationID &&
                    this.ReservationDate == objRes.ReservationDate &&
                    this.ArrivalDate == objRes.ArrivalDate &&
                    this.DepartureDate == objRes.DepartureDate &&
                    this.SettledIn == objRes.SettledIn &&
                    this.RoomReservation.Equals(objRes.RoomReservation) &&
                    this.ClientResevation.Equals(objRes.ClientResevation);  /*&&*/
                    //this.UserReservation.Equals(objRes.UserReservation);
            }
            return base.Equals(obj);
        }
    }
}
