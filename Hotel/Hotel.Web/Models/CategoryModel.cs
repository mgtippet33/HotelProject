using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hotel.Web.Models
{
    public class CategoryModel
    {
        public int CategoryID { set; get; }
        [DisplayName("Category Name")]
        public string CategoryName { set; get; }
        [DisplayName("Lying places")]
        public int Capacity { set; get; }
    }
}