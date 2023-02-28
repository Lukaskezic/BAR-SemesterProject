using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarDatabase.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int AmountInStock { get; set; }
        public string ImgUrl { get; set; }
        public string Recipe { get; set; }

        #region Relationships
        
        public ICollection<Order> Orders { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        public List<ProductIngredient> ProductIngredients { get; set; }

        #endregion

    }
}
