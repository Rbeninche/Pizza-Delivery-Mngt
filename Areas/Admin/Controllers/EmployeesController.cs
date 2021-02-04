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
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        [TempData]
        public string StatusMessage { get; set; }

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.Where(e=> e.Terminated==false).ToListAsync());
        }

        // GET: Admin/Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                //Checks if Employee has already been added by Admin.

                var checksIfLastNameExists = _context.Employees.FirstOrDefault(
                c => c.LastName == employee.LastName
             && c.LastName.ToLower().Contains(employee.LastName.ToLower())
             && c.FirstName == employee.FirstName
             && c.FirstName.ToLower().Contains(employee.FirstName.ToLower())

             );

                var terminatedEmployee = _context.Employees.Where(c=>c.FirstName == employee.FirstName
                && c.LastName == employee.LastName
                && c.Terminated == true).Any();

              
                  if (checksIfLastNameExists != null && terminatedEmployee == true)
                {

                    //Error
                    StatusMessage = "Error: Employee " + checksIfLastNameExists.FirstName + " " + checksIfLastNameExists.LastName + " has been terminated.";
                  
                }
                else if (checksIfLastNameExists != null)
                {
                    //Error
                    StatusMessage = "Error: Employee already in the system under " + checksIfLastNameExists.FirstName + " "
                        + checksIfLastNameExists.LastName;

                }
                else
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                
            }

            ViewBag.StatusMessage = StatusMessage;
            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Terminated")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Checks if Employee has already been added by Admin.

                var checksIfLastNameExists = _context.Employees.FirstOrDefault(
                  c => c.LastName == employee.LastName
               && c.LastName.ToLower().Contains(employee.LastName.ToLower())
               && c.FirstName == employee.FirstName
               && c.FirstName.ToLower().Contains(employee.FirstName.ToLower())
               && c.Id != employee.Id

               );

                var terminatedEmployee = _context.Employees.Where(c => c.FirstName == employee.FirstName
                && c.LastName == employee.LastName
                && c.Terminated == true).Any();

                if (checksIfLastNameExists != null && terminatedEmployee == true)
                {

                    //Error
                    StatusMessage = "Error: Employee " + checksIfLastNameExists.FirstName + " " + checksIfLastNameExists.LastName + " has been terminated.";

                }
                else if (checksIfLastNameExists != null)
                {
                    //Error
                    StatusMessage = "Error: Employee already in the system under " + checksIfLastNameExists.FirstName + " "
                        + checksIfLastNameExists.LastName;

                }
                else
                {
                    try
                    {
                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(employee.Id))
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
               
            }
            ViewBag.StatusMessage = StatusMessage;
            return View(employee);
        }

        // GET: Admin/Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
