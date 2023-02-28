using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BarDatabase.Controllers;
using BarDatabase.Migrations;
using BarDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;

// TODO 
// v Hente næste ordre (der ikker er leveret) der kan vise antal drinks og hvilke
// v Hente Kogebog for, hvordan man laver drinks, ud fra et Produkt
// v Finde Bartender ud fra Username (login)
// v Forbinde en bartender til en Ordre, når den er færdig
// v Fortæl at ordre er leveret

namespace BarDatabase.Controllers
{
    public class BarDbController
    {
        private readonly Context _context;

        public BarDbController(Context context)
        {
            _context = context;
        }

        // ORDERS

        // Add order to database
        public void AddOrder(Order order)
        {
            if (Exists(order)) return;

            _context.Orders.Add(order);

            _context.SaveChanges();
        }

        // Update an oprder objekt to the database
        public void UpdateOrder(Order order)
        {
            if (!Exists(order)) return;

            _context.Orders.Update(order);

            _context.SaveChanges();
        }

        // Delete an order object from database. Found via Id
        public void DeleteOrder(Order order)
        {
            if (!Exists(order)) return;

            _context.Orders.Remove(order);

            _context.SaveChanges();
        }

        // Gets the Order in database with certain Id
        public Order GetOrderById(int id)
        {
            return _context.Orders.Single(o => o.OrderId == id);
        }

        public bool Exists(Order order)
        {
            return _context.Orders.Any(o => o.OrderId == order.OrderId);
        }


        // CUSTOMER

        public void AddCustomer(Customer customer)
        {
            if (Exists(customer)) return;

            _context.Customers.Add(customer);

            _context.SaveChanges();
        }

        public Customer GetCustomerByName(string name)
        {
            return _context.Customers.First(c => c.FirstName == name);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public bool Exists(Customer customer)
        {
            return _context.Customers.Any(c => c.Email == customer.Email);
        }

        // BARTENDER

        public void AddBartender(Bartender bartender)
        {
            if (Exists(bartender)) return;

            _context.Bartenders.Add(bartender);

            _context.SaveChanges();
        }

        public Bartender GetBartenderByName(string name)
        {
            return _context.Bartenders.First(b => b.Name == name);
        }

        public bool Exists(Bartender bartender)
        {
            return _context.Bartenders.Any(b => b.Username == bartender.Username);
        }

        // PRODUCT

        public void AddProduct(Product product)
        {
            if (Exists(product)) return;

            _context.Products.Add(product);

            _context.SaveChanges();
        }

        public bool Exists(Product product)
        {
            return _context.Products.Any(p => p.Name == product.Name);
        }

        public Product GetProductByName(string name)
        {
            return _context.Products
                .Include(p => p.ProductIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .First(p => p.Name == name);
        }

        // INGREDIENTS

        public void AddIngredient(Ingredient ingredient)
        {
            if (Exists(ingredient)) return;

            _context.Ingredients.Add(ingredient);

            _context.SaveChanges();
        }

        public bool Exists(Ingredient ingredient)
        {
            return _context.Ingredients.Any(i => i.Name == ingredient.Name);
        }

        public Ingredient GetIngredientByName(string name) => 
            _context.Ingredients.First(i => i.Name == name);


        // CATEGORY

        public void AddCategory(Category category)
        {
            if (Exists(category)) return;

            _context.Categories.Add(category);

            _context.SaveChanges();
        }

        public Category GetCategoryByType(string type)
        {
            return _context.Categories.Single(c => c.Type == type);
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public bool Exists(Category category)
        {
            return _context.Categories.Any(c => c.Type == category.Type);
        }

        // DEBUGGING

        // Add static data, meant for testing purpose
        public void AddStaticTestData()
        {
            AddCustomer(new Customer() { Email = "mainmail@yas.dk", Password = "123", FirstName = "Gustav", LastName = "Knudsen" });
            AddCustomer(new Customer() { Email = "dinmail@yas.dk", Password = "123", FirstName = "Moller", LastName = "Mollersen" });
            AddCustomer(new Customer() { Email = "voresmail@google.com", Password = "123", FirstName = "Bach", LastName = "Bachsen" });
            AddCustomer(new Customer() { Email = "deresmail@yas.com", Password = "123", FirstName = "Simon", LastName = "Dang-man" });
            
            AddBartender(new Bartender() { IsAdmin = true, Name = "Søren", Username = "srn", Password = "1234"});
            AddBartender(new Bartender() { IsAdmin = false, Name = "Mikkel", Username = "mkl", Password = "4321" });

            AddCategory(new Category() { Type = "Beer"});
            AddCategory(new Category() { Type = "G&T" });   
            AddCategory(new Category() { Type = "Vodka" });

            AddIngredient(new Ingredient() { Name = "Sugar" });
            AddIngredient(new Ingredient() { Name = "Vodka" });
            AddIngredient(new Ingredient() { Name = "Lemon Juice" });
            AddIngredient(new Ingredient() { Name = "Gin" });
            AddIngredient(new Ingredient() { Name = "Tonic Water" });
            AddIngredient(new Ingredient() { Name = "Cucumber" });

            // TODO Fobidnelse til categories virker ikke ordentligt

            AddProduct(new Product() 
            {
                Name = "Carlsberg 50cl", 
                ImgUrl = "https://media-verticommnetwork1.netdna-ssl.com/wines/24-x-carlsberg-danish-pilsner-50cl-1513713-s508.jpg",
                Price = 25, 
                AmountInStock = 10,
                Categories = new List<Category>(){GetCategoryByType("Beer")}
            });
            AddProduct(new Product() 
            {
                Name = "Tuborg 75cl", 
                ImgUrl = "https://spritbox.com/media/catalog/product/cache/3/image/cbf810cc3c4b47b0065451446fc91d66/t/u/tuborg_classic.jpg",
                Price = 30, 
                AmountInStock = 30,
                Categories = new List<Category>() { GetCategoryByType("Beer") }
            });
            AddProduct(new Product() 
            {
                Name = "Gin & Tonic", 
                ImgUrl = "https://cdn.diffords.com/contrib/stock-images/2016/8/07/201642335eb2fd8d5d9dc53a1fccb8dd1015.jpg",
                Price = 50,
                AmountInStock = 5,
                Categories = new List<Category>() { GetCategoryByType("G&T") },
                ProductIngredients = new List<ProductIngredient>()
                {
                    new ProductIngredient() {Amount = 4, MeasurementType = "cl", Ingredient = GetIngredientByName("Gin")},
                    new ProductIngredient() {Amount = 18, MeasurementType = "cl", Ingredient = GetIngredientByName("Tonic Water")},
                    new ProductIngredient() {Amount = 2, MeasurementType = "slices", Ingredient = GetIngredientByName("Cucumber")}
                }
            });
            AddProduct(new Product() 
            {
                Name = "Vodka Juice", 
                Price = 45 ,
                AmountInStock = 20,
                ImgUrl = "https://www.saq.com/media/catalog/product/s/c/screwdriver-ec-1_1610405159.png?quality=80&fit=bounds&height=&width=",
                Categories = new List<Category>() { GetCategoryByType("Vodka") },
                ProductIngredients = new List<ProductIngredient>()
                {
                    new ProductIngredient() {Amount = 2, MeasurementType = "Tbsp", Ingredient = GetIngredientByName("Sugar")}, 
                    new ProductIngredient() {Amount = 2, MeasurementType = "cl", Ingredient = GetIngredientByName("Vodka")},
                    new ProductIngredient() {Amount = 20, MeasurementType = "ml", Ingredient = GetIngredientByName("Lemon Juice")},
                }
            });
        }

        // Add Orders test funcitonality
        public void AddTestOrders()
        {
            _context.Set<Order>().Add(new Order()
            {
                TableNo = 4,
                Bartender = _context.Bartenders.First(),
                Customer = _context.Customers.First(),
                ProductOrders = new List<ProductOrder>()
                {
                    new ProductOrder()
                    {
                        Amount = 2,
                        Product = _context.Products.First()
                    },
                    new ProductOrder()
                    {
                        Amount = 1,
                        Product = GetProductByName("Tuborg 75cl")
                    },
                }
            });

            _context.Set<Order>().Add(new Order()
            {
                TableNo = 4,
                Bartender = _context.Bartenders.First(),
                Customer = _context.Customers.First(),
                ProductOrders = new List<ProductOrder>()
                {
                    new ProductOrder()
                    {
                        Amount = 2,
                        Product = GetProductByName("Vodka Juice")
                    },
                    new ProductOrder()
                    {
                        Amount = 1,
                        Product = GetProductByName("Gin & Tonic")
                    },
                }
            });

            _context.SaveChanges();
        }

    }
}
