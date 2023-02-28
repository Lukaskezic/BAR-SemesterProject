using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarDatabase.Models
{
    public class Bartender
    {
        public int BartenderId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }

        #region Relationships

        public List<Order> Orders { get; set; }

        public List<Shift> Shifts { get; set; }

        #endregion

    }
}
