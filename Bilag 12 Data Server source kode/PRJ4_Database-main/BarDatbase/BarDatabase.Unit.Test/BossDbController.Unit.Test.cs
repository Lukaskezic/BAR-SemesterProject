using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using BarDatabase;
using BarDatabase.Controllers;
using BarDatabase.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BarDatabase.Unit.Test
{
    public class BossDbControllerTest
    {
        private Context context;
        private BarDbController db;
        private BossDbController bossDb;

        [SetUp]
        public void Setup()
        {
            context = new Context();
            db = new BarDbController(context);
            bossDb = new BossDbController(context);

            if (context.Orders.Any()) return;

            db.AddStaticTestData();
            db.AddTestOrders();
        }

        [Test]
        public void GetProductWithIngredients_GetProduct_ProductIncludesIngredients()
        {
            var product = bossDb.GetProductWithIngredients(context.Products.Single(p => p.Name == "Vodka Juice"));
            Assert.IsNotNull(product.Ingredients);
        }

        [Test]
        public void GetBartenderByUsername_GetWrongBartender_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => bossDb.GetBartenderByUsername("234124fsdf"));
        }

        [Test]
        public void GetBartenderByUsername_GetBartender_BartenderNotNull()
        {
            var bartender = bossDb.GetBartenderByUsername("srn");
            
            Assert.NotNull(bartender);
        }
        
        [Test]
        public void BartenderFinishedOrder_SetOrderAsFinished_OrderHasDeliveredDate()
        {
            var tableNo = 332;
            context.Orders.Add(new Order() {TableNo = tableNo});
            context.SaveChanges();

            var order = bossDb.GetNextNotCompletedOrder();
            var bartender = context.Bartenders.First();
            bossDb.BartenderFinishedOrder(order, bartender);

            Assert.IsFalse(context.Orders.Any(o => o.TableNo == tableNo && o.DeliveryTime != DateTime.MinValue));
        }

        [Test]
        public void StartBartenderShift_StartShift_ShiftIsCreated()
        {
            var bartender = context.Bartenders.First();

            bossDb.StartBartenderShift(bartender);
        }
    }
}
