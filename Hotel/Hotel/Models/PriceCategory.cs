using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class PriceCategory
    {
        [Key]
        public int PriceCategoryID { set; get; }
        public int CategoryID { set; get; }
        public decimal Price { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }

        [ForeignKey("CategoryID")]
        public virtual Category CategoryName { set; get; }
    }
}