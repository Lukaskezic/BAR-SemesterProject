using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarDatabase;
using BarDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace BarDatabase.Controllers
{
    public class BossDbController : BarDbController
    {
        private readonly Context _context;

        public BossDbController(Context context) : base(context)
        {
            _context = context;
        }

        // Request

        // Returns the next not completed order.
        // If all Orders has been completed, this wil throw an exception
        public Order GetNextNotCompletedOrder()
        {
            var nextOrder = _context.Orders
                .Include(p => p.ProductOrders)
                .ThenInclude(po => po.Product)
                .First(o => o.DeliveryTime == DateTime.MinValue);

            return nextOrder;
        }

        //Gets the product object from database, but including the Ingredients table
        public Product GetProductWithIngredients(Product product)
        {
            return _context.Products
                .Include(p => p.ProductIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .Single(p => p.ProductId == product.ProductId);
        }

        // For Login purpose. Get the Bartender, via username
        public Bartender GetBartenderByUsername(string username)
        {
            return _context.Bartenders.Single(b => b.Username == username);
        }

        // Commands
        
        // When a Bartender finishes the order, this is run.
        // Adds the bartender to Order, and sets DeliveryTime
        public void BartenderFinishedOrder(Order order, Bartender bartender)
        {
            var finishedOrder = _context.Orders.Single(o => o.OrderId == order.OrderId);

            finishedOrder.BartenderId = bartender.BartenderId;
            finishedOrder.DeliveryTime = DateTime.Now;

            _context.SaveChanges();
        }

        // Adds a Shift element to a Bartender.
        // If a shift already is defined, does nothing
        public void StartBartenderShift(Bartender bartender)
        {
            var myBartender = _context.Bartenders.Include(b => b.Shifts).Single(b => b.Username == bartender.Username);
            
            if (myBartender.Shifts.Exists(s => s.EndTime == DateTime.MinValue)) 
                return;
            myBartender.Shifts.Add(new Shift() { StartTime = DateTime.Now });

            _context.SaveChanges();
        }

        // End a shift, that does not have an EndTime
        // If no Shifts miss EndTime, does nothing
        public void EndBartenderShift(Bartender bartender)
        {
            _context.Bartenders
                .Single(b => b.Username == bartender.Username)
                .Shifts
                .Single(s => s.EndTime == DateTime.MinValue)
                .EndTime = DateTime.Now;

            _context.SaveChanges();
        }

        // Adds a bartender to the database.
        // Does nothing, if bartender already exists
        public void AddNewBartender(Bartender bartender)
        {
            if (Exists(bartender)) return;

            _context.Bartenders.Add(bartender);

            _context.SaveChanges();
        }
    }
}