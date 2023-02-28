using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BarDatabase.Models
{
    public class ProductIngredient
    {
        public string MeasurementType { get; set; }
        public int Amount { get; set; }

        #region Relationships

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        #endregion
    }
}
