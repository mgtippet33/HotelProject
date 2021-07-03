using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Room
    {
        [Key]
        public int RoomID { set; get; }
        public string RoomName { set; get; }
        public int PriceCategoryID { set; get; }
        public bool Active { set; get; }

        [ForeignKey("PriceCategoryID")]
        public virtual PriceCategory RoomCategory { set; get; }
    }
}