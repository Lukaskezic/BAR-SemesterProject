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
    public class BarDbControllerTest
    {
        private Context context;
        private BarDbController db;

        [SetUp]
        public void Setup()
        {
            context = new Context();
            db = new BarDbController(context);

            if (context.Orders.Any()) return;

            db.AddStaticTestData();
            db.AddTestOrders();
        }

        [Test]
        public void AddOrder_AddingANewOrder_OrderAdded()
        {
            var now = DateTime.Now;
            db.AddOrder(
                new Order(){
                    Bartender = context.Bartenders.First(), 
                    Products = new List<Product>() { context.Products.First()},
                    TableNo = 2,
                    CreationTime = now
                }
            );

            Assert.IsTrue(context.Orders.Any(o => o.CreationTime == now));
        }

        [Test]
        public void GetOrderById_GetExistingOrderById_ExistingOrderReturned()
        {
            var existingOrder = context.Orders.First();

            var order = db.GetOrderById(existingOrder.OrderId);

            Assert.IsTrue(order.OrderId == existingOrder.OrderId);
        }

        [Test]
        public void GetOrderById_GetNonExistingOrderById_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => db.GetOrderById(432423423));
        }

        [Test]
        public void UpdateOrder_SetNewTableNo_OrderIsUpdated()
        {
            var existingOrder = context.Orders.First();
            const int newTableNo = 10;
            existingOrder.TableNo = newTableNo;

            db.UpdateOrder(existingOrder);

            Assert.IsTrue(context.Orders.Single(o => o.OrderId == existingOrder.OrderId).TableNo == newTableNo);
        }

        [Test]
        public void DeleteOrder_DeleteCreatedOrder_OrderIsDeleted()
        {
            // Add test data
            var now = DateTime.Now;
            context.Orders.Add(new Order() {CreationTime = now});
            context.SaveChanges();

            db.DeleteOrder(context.Orders.Single(o => o.CreationTime == now));

            Assert.Throws<InvalidOperationException>(() => context.Orders.Single(o => o.CreationTime == now));
        }

        [Test]
        public void DeleteOrder_DeleteNonExistingOrder_FunctionCanFinish()
        {
            db.DeleteOrder(new Order() { CreationTime = DateTime.Now });
        }

        [Test]
        public void OrderExists_ExistingOrderChecked_True()
        {
            var order = context.Orders.First();

            Assert.IsTrue(db.Exists(order));
        }

        [Test]
        public void OrderExists_NonExistingOrderChecked_False()
        {
            Assert.IsFalse(db.Exists(new Order() {TableNo = 132344}));
        }
    }   
}