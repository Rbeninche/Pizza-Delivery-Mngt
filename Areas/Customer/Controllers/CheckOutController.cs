using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaDeliveryManagement.Data;
using PizzaDeliveryManagement.Helpers;
using PizzaDeliveryManagement.Models;

namespace PizzaDeliveryManagement.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CheckOutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckOutController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Menu.Price * item.Quantity);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(OrderCustomerInfo orderCustomerInfo)
        {
            if (ModelState.IsValid)
            {


                _context.Add(orderCustomerInfo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));


            }

            return View(orderCustomerInfo);
        }
    }
}