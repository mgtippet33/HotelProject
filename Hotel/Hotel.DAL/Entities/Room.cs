using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Room
    {
        [Key]
        public int RoomID { set; get; }
        [Required]
        public string RoomName { set; get; }
        public int PriceCategoryID { set; get; }
        public bool Active { set; get; }

        [ForeignKey("PriceCategoryID")]
        public virtual PriceCategory RoomCategory { set; get; }

        public override bool Equals(object obj)
        {
            if(obj is Room)
            {
                var objRoom = obj as Room;
                return this.RoomID == objRoom.RoomID &&
                    this.Active == objRoom.Active &&
                    this.RoomCategory.Equals(objRoom.RoomCategory);

            }
            return base.Equals(obj);
        }
    }
}
