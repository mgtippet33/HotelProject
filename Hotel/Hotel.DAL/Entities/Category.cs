using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Category
    {
        [Key]
        public int CategoryID { set; get; }
        [Required]
        public string CategoryName { set; get; }
        [Required]
        public int Capacity { set; get; }

        public string ActionUserName { get; set; }
        public string ActionType { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ActionTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Category)
            {
                var objCategory = obj as Category;
                return this.CategoryID == objCategory.CategoryID &&
                    this.CategoryName == objCategory.CategoryName &&
                    this.Capacity == objCategory.Capacity;
            }
            return base.Equals(obj);
        }
    }
}
