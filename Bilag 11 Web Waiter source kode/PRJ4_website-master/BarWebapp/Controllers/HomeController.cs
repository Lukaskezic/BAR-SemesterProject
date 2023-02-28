using BarDatabase.Models;
using BarWebapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BarDatabase;
using BarDatabase.Controllers;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http.Extensions;

namespace BarWebapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Context context;
        public WebWaiterDbController _dbController;
        private Context _dbModel;
        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _dbController = new WebWaiterDbController(context);
            _dbModel = context;
        }

        public IActionResult Index()
        {
	        return View();
        }

        public IActionResult About()
        {
	        return View();
        }

        public IActionResult Cart()
        {
	        return View();
        }
        //ConfirmOrder funktion der sender ordre i database
        [HttpPost]
        public IActionResult ConfirmOrder(int id)
        {
	        string result = new StreamReader(Request.Body).ReadToEndAsync().Result;
            List<WebOrder> WebordersInOrder = JsonConvert.DeserializeObject<List<WebOrder>>(result);

            //make order
            Order newOrder = new Order(){
	            TableNo = id, 
	            CreationTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddMinutes(10),
                ProductOrders = new List<ProductOrder>(),
            };

            foreach (var webproduct in WebordersInOrder)
            {
	            Product _product = _dbModel.Products.Single(p => p.ProductId == webproduct.id);

	            newOrder.AddProductToOrder(_product, webproduct.amount);
            }

            _dbController.AddNewOrder(newOrder);

            return Accepted();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
