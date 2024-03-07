using Microsoft.AspNetCore.Mvc;
using PracticalMVC.Models;
using System.Diagnostics;
using PracticalMVC;
using PracticalMVC.Repositories;

namespace PracticalMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //Get Employees
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return View(employees);
        }

        //Get InsertOrUpdate
        public IActionResult InsertOrUpdate(int? id)
        {
            Employee employee = new Employee();
            if(id.HasValue && id > 0)
            {
                employee = _employeeRepository.GetEmployeeById(id.Value);
                if(employee == null)
                {
                    employee = new Employee();
                }
            }
            return View(employee);
        }

        //Post InsertOrUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdate(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.InsertOrUpdateEmployee(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error occured: {ex.Message}");
                }
            }
            
            return View(employee) ;
        }

        //Delete an employee
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return Json(new { success = true, message = "Employee deleted successfully." });
        }
    }
}
