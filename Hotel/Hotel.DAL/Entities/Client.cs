using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Entities
{
    public class Client
    {
        [Key]
        public int ClientID { set; get; }
        [Required]
        public string Surname { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Passport { set; get; }
    }
}
