using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hotel.Web.Models
{
    public class PriceCategoryModel
    {
        public int PriceCategoryID { set; get; }
        [DisplayName("Name of category")]
        public CategoryModel CategoryName { set; get; }
        [DisplayName("Price of category")]
        public decimal Price { set; get; }
        [DisplayName("Start of category")]
        public DateTime StartDate { set; get; }
        [DisplayName("End of category")]
        public DateTime EndDate { set; get; }
    }
}