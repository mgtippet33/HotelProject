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

        public override bool Equals(object obj)
        {
            if (obj is RoomModel)
            {
                var objRoom = obj as RoomModel;
                return this.RoomID == objRoom.RoomID &&
                    this.Active == objRoom.Active &&
                    this.RoomCategory.Equals(objRoom.RoomCategory);

            }
            return base.Equals(obj);
        }
    }
}