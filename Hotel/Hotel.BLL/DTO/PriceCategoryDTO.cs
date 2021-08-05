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

        public string ActionUserName { get; set; }
        public string ActionType { get; set; }
        public DateTime ActionTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is PriceCategoryDTO)
            {
                var objPriceCategory = obj as PriceCategoryDTO;
                return this.PriceCategoryID == objPriceCategory.PriceCategoryID &&
                    this.Price == objPriceCategory.Price &&
                    this.StartDate == objPriceCategory.StartDate &&
                    this.EndDate == objPriceCategory.EndDate &&
                    this.CategoryName.Equals(objPriceCategory.CategoryName);
            }
            return base.Equals(obj);
        }
    }
}
