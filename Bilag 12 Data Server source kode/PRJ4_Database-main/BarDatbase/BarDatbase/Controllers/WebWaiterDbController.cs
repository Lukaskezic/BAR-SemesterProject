using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarDatabase.Models;
using Microsoft.EntityFrameworkCore;

// TODO
// v Hente alle produkter
// v Se om produkter er på lager
// v Tilføj kategorier til Produkter, (eks. søge efter øl)
// v Funktioner til at sortere (navn, pris etc.)
// v Opret Customer
// v Tilføj ny ordre (Fjern antal produkter fra lager)
// * Hente Customer ud email
// * 

namespace BarDatabase.Controllers
{
    public class WebWaiterDbController : BarDbController
    {
        private readonly Context _context;

        public WebWaiterDbController(Context context) : base(context)
        {
            _context = context;
        }

        // Returns all Products in database
        public List<Product> GetAllProducts()
        {
            return _context.Products
                .Include(p => p.Categories)
                .ToList();
        }

        // Gets all the products in a certain category.
        // If wrong categroy name, throws exception
        public List<Product> GetAllProductInCategory(string categoryType)
        {
            return _context.Categories
                .Single(c => c.Type == categoryType).Products.ToList();
        }

        // Gets all Products, ordered by name
        public List<Product> GetAllProductsOrderedByName()
        {
            return _context.Products
                .Include(p => p.Categories)
                .OrderBy(p => p.Name)
                .ToList();
        }

        // Gets all Products, ordered by price
        public List<Product> GetAllProductsOrderedByPrice()
        {
            return _context.Products
                .Include(p => p.Categories)
                .OrderBy(p => p.Price)
                .ToList();
        }

        // Gets a customer from their email
        public Customer GetExistingCustomer(string email)
        {
            return _context.Customers.Single(c => c.Email == email);
        }

        // Commands

        // Adds a new customer to database
        // Does nothing if customer already exists
        public void AddNewCustomer(Customer customer)
        {
            if (Exists(customer) ) return;

            _context.Customers.Add(customer);

            _context.SaveChanges();
        }

        // A e new order to the database,
        // and decrements the stock, from Products in Order
        public void AddNewOrder(Order order)
        {
            if (Exists(order)) return;

            _context.Orders.Add(order);

            //Remove amount from stock
            foreach (var item in order.ProductOrders)
            {
                var product = item.Product;
                product.AmountInStock -= item.Amount;
            }

            _context.SaveChanges();
        }

        // Updates an existing order
        // Does nothing, if Order does not exist
        public void UpdateExistingOrder(Order updatedOrder)
        {
            if (!Exists(updatedOrder)) return;

            var oldOrder = _context.Orders.Single(o => o.OrderId == updatedOrder.OrderId);
            oldOrder = updatedOrder;

            _context.SaveChanges();
        }



    }
}
