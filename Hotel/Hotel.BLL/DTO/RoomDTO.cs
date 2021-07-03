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
    }
}
