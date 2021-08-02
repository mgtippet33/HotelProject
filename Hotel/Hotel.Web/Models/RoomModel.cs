using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hotel.Web.Models
{
    public class RoomModel
    {
        public int RoomID { set; get; }
        [DisplayName("Name of room")]
        public string RoomName { set; get; }
        [DisplayName("Category of room")]
        public PriceCategoryModel RoomCategory { set; get; }
        public bool Active { set; get; }
    }
}