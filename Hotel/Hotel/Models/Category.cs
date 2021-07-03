using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { set; get; }
        public string CategoryName { set; get; }
        public int Capacity { set; get; }
    }
}