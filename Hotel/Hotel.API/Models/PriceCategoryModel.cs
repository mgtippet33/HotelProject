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

        public override bool Equals(object obj)
        {
            if(obj is PriceCategoryModel)
            {
                var objPriceCategory = obj as PriceCategoryModel;
                return this.PriceCategoryID == objPriceCategory.PriceCategoryID &&
                    this.Price == objPriceCategory.Price &&
                    this.StartDate == objPriceCategory.StartDate &&
                    this.EndDate == objPriceCategory.EndDate &&
                    this.CategoryName.Equals(objPriceCategory.CategoryName); ;
            }
            return base.Equals(obj);
        }
    }
}