using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayRollMVC.Controllers
{
    public class EmpController : Controller
    {
        private readonly IEmpBusiness empBusiness;

        public EmpController(IEmpBusiness empBusiness)
        {
            this.empBusiness = empBusiness;
        }
        public IActionResult Index()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            list = empBusiness.GetAllEmployees().ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind] EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                empBusiness.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
