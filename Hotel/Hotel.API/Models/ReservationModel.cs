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

        public override bool Equals(object obj)
        {
            if (obj is ReservationModel)
            {
                var objRes = obj as ReservationModel;
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