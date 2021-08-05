using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class PriceCategory
    {
        [Key]
        public int PriceCategoryID { set; get; }
        public int CategoryID { set; get; }
        [Required]
        public decimal Price { set; get; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { set; get; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { set; get; }

        public string ActionUserName { get; set; }
        public string ActionType { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ActionTime { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category CategoryName { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is PriceCategory)
            {
                var objPriceCategory = obj as PriceCategory;
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
