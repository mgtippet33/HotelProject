using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class RoomModel
    {
        public int RoomID { set; get; }
        public string RoomName { set; get; }
        public PriceCategoryModel RoomCategory { set; get; }
        public bool Active { set; get; }
    }
}