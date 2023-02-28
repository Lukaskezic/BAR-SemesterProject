using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarDatabase.Models
{
    public class ProductOrder
    {
        public int Amount { get; set; }

        #region Relationships

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        #endregion

    }
}
