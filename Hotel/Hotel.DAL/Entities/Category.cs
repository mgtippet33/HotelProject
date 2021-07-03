using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Capacity { set; get; }
    }
}
