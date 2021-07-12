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

        public override bool Equals(object obj)
        {
            if(obj is CategoryModel)
            {
                var objCategory = obj as CategoryModel;
                return this.CategoryID == objCategory.CategoryID &&
                    this.CategoryName == objCategory.CategoryName &&
                    this.Capacity == objCategory.Capacity;
            }
            return base.Equals(obj);
        }
    }
}