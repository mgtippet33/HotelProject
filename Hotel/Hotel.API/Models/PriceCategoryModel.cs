using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class PriceCategoryModel
    {
        public int PriceCategoryID { set; get; }
        public CategoryModel CategoryName { set; get; }
        public decimal Price { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}