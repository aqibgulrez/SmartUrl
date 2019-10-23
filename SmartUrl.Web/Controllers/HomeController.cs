using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUrl.Entities.Domain;
using SmartUrl.Web.Models;

namespace SmartUrl.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IOrderRepository _orderRepository;
 
        //public HomeController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        //{
        //    _orderRepository = orderRepository;
        //    _shoppingCart = shoppingCart;
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SmartUrlEntity objSmartUrlEntity)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
