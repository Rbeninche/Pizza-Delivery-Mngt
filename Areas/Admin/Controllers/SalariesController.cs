using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaDeliveryManagement.Data;
using PizzaDeliveryManagement.Models;
using PizzaDeliveryManagement.Utility;

namespace PizzaDeliveryManagement.Areas.Admin.Controllers
{
    [Authorize(Roles = StaticData.AdminUser)]
    [Area("Admin")]
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        [TempData]
        public string StatusMessage { get; set; }

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Salaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Salary.Include(s => s.Employee);
            return View(await applicationDbContext.Where(e=> e.Employee.Terminated == false).ToListAsync());
        }

        // GET: Admin/Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // GET: Admin/Salaries/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(e => e.Terminated == false), "Id", "FriendlyName");
            return View();
        }

        // POST: Admin/Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SalaryPerHour,EmployeeId")] Salary salary)
        {
            if (ModelState.IsValid)
            {

                //Checks if Employee already Has a Salary

                var checksEmployeeSalary = _context.Salary.Include(e => e.Employee).Where(e=> e.Employee.Id == salary.EmployeeId).Any();

                if(checksEmployeeSalary == true)
                {
                    //Error
                    StatusMessage = "Error : Employee already has salary in database. You can go and update it now";
                }
                else
                {
                    _context.Add(salary);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(e => e.Terminated == false), "Id", "FirstName", salary.EmployeeId);
            ViewBag.StatusMessage = StatusMessage;
            return View(salary);
        }

        // GET: Admin/Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(e => e.Terminated == false && e.Id == salary.EmployeeId), "Id", "FriendlyName");
            return View(salary);
        }

        // POST: Admin/Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SalaryPerHour,EmployeeId")] Salary salary)
        {
            if (id != salary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                
                    try
                    {
                        _context.Update(salary);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SalaryExists(salary.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
               
                
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees.Where(e => e.Terminated == false), "Id", "FriendlyName");
            ViewBag.StatusMessage = StatusMessage;
            return View(salary);
        }

        // GET: Admin/Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Admin/Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = await _context.Salary.FindAsync(id);
            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salary.Any(e => e.Id == id);
        }
    }
}
