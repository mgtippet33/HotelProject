using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class PriceCategoryDTO
    {
        public int PriceCategoryID { set; get; }
        public CategoryDTO CategoryName { set; get; }
        public decimal Price { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}
