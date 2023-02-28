using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarDatabase.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Type;

        public ICollection<Product> Products { get; set; }
    }
}
