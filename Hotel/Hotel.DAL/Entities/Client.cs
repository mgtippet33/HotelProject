using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string ActionUserName { get; set; }
        public string ActionType { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ActionTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Client)
            {
                var objCl = obj as Client;
                return this.ClientID == objCl.ClientID && this.Surname == objCl.Surname &&
                    this.Name == objCl.Name && this.Passport == objCl.Passport;
            }
            return base.Equals(obj);
        }
    }
}
