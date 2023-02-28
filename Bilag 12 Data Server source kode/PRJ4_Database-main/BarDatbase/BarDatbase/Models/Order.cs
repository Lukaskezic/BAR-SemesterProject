using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarDatabase.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public int TableNo { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeliveryTime { get; set; }

        #region Relations

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? BartenderId{ get; set; }
        public Bartender? Bartender { get; set; }

        public ICollection<Product> Products { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        #endregion

        public void 
            
            AddProductToOrder(Product product, int amount)
        {
            ProductOrders.Add(new ProductOrder() {Amount = amount, Product = product});
        }
    }
}
