using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarDatabase.Models;

namespace BarWebapp.Models
{
	public class OrderViewModel
	{
		public List<Product> listOfProducts { get; set;}
		public List<Category> categories { get; set; }
	}
}
