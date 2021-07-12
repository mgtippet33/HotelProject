using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class ReservationDTO
    {
        public int ReservationID { set; get; }
        public RoomDTO RoomReservation { set; get; }
        public ClientDTO ClientResevation { set; get; }
        public DateTime? ReservationDate { set; get; }
        public DateTime ArrivalDate { set; get; }
        public DateTime DepatureDate { set; get; }
        public bool SettledIn { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is ReservationDTO)
            {
                var objRes = obj as ReservationDTO;
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
