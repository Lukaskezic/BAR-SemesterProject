using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarDatabase.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
        public List<ProductIngredient> ProductIngredients { get; set; }
    }
}
