using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class RoomDTO
    {
        public int RoomID { set; get; }
        public string RoomName { set; get; }
        public PriceCategoryDTO RoomCategory { set; get; }
        public bool Active { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is RoomDTO)
            {
                var objRoom = obj as RoomDTO;
                return this.RoomID == objRoom.RoomID &&
                    this.Active == objRoom.Active &&
                    this.RoomCategory.Equals(objRoom.RoomCategory);

            }
            return base.Equals(obj);
        }
    }
}
