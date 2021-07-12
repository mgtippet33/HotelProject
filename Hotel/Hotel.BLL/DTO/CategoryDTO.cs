using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class CategoryDTO
    {
        public int CategoryID { set; get; }
        public string CategoryName { set; get; }
        public int Capacity { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is CategoryDTO)
            {
                var objCategory = obj as CategoryDTO;
                return this.CategoryID == objCategory.CategoryID &&
                    this.CategoryName == objCategory.CategoryName &&
                    this.Capacity == objCategory.Capacity;
            }
            return base.Equals(obj);
        }
    }
}
