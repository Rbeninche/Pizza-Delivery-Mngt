using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaDeliveryManagement.Data;

namespace PizzaDeliveryManagement.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menu.Include(s=>s.PizzaSize).ToListAsync());
        }

     
    }
}