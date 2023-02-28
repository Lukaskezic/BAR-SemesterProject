using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarDatabase;
using BarDatabase.Controllers;
using BarDatabase.Models;
using BarWebapp.Models;

namespace BarWebapp.Controllers
{
    public class OrderController : Controller
    {

	    private Context _context;
	    public WebWaiterDbController _dbController;
        

        public OrderController(Context context)
	    {

		    _context = context;
		    _dbController = new WebWaiterDbController(_context);
	    }


	    // GET: OrderController
        public ActionResult Order()
        {
	        OrderViewModel vm = new OrderViewModel();

	        //Få database data ind i OrderViewModel
            vm.listOfProducts = _dbController.GetAllProductsOrderedByPrice();
            vm.categories = _dbController.GetAllCategories(); 

	        return View(vm);
        }

    }
}
