using System;
using System.Collections.Generic;
using System.Linq;
using BarDatabase.Models;
using Microsoft.AspNetCore.Builder;
using BarDatabase.Controllers;

namespace BarDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new Context("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BarTest; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            var bd = new BarDbController(context);

            bd.AddStaticTestData();
            bd.AddTestOrders();
        }
    }
}
