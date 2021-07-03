using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.API.Models
{
    public class CategoryModel
    {
        public int CategoryID { set; get; }
        public string CategoryName { set; get; }
        public int Capacity { set; get; }
    }
}