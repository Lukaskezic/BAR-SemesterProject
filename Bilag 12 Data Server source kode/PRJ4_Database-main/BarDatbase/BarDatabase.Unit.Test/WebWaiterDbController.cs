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
    public class WebWaiterDbControllerTest
    {
        private Context context;
        private BarDbController db;
        private WebWaiterDbController webWaiterDb;

        [SetUp]
        public void Setup()
        {
            context = new Context();
            db = new BarDbController(context);
            webWaiterDb = new WebWaiterDbController(context);

            if (context.Orders.Any()) return;

            db.AddStaticTestData();
            db.AddTestOrders();
        }

        // GetAllProductsOrderedByPRice
        // GetAllCategories

        [Test]
        public void AddNewOrder_AddOrderWithCurrentTime_OrderIsAdded()
        {
            var now = DateTime.Now;
            db.AddOrder(
                new Order()
                {
                    Bartender = context.Bartenders.First(),
                    Products = new List<Product>() { context.Products.First() },
                    TableNo = 2,
                    CreationTime = now
                }
            );

            Assert.IsTrue(context.Orders.Any(o => o.CreationTime == now));
        }

        [Test]
        public void AddProductToOrder_AddProduct_()
        {
            var products = webWaiterDb.GetAllProductsOrderedByPrice();

            Assert.GreaterOrEqual(products.Last().Price, products.First().Price);
        }

        [Test]
        public void GetAllCategories_GetAllCategories_CategoriesReceived()
        {
            var categories = webWaiterDb.GetAllCategories();

            Assert.NotNull(categories);
        }

    }
}
