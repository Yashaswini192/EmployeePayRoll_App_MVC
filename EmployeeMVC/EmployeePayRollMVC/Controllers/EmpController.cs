using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System;
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = empBusiness.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]       
        public IActionResult Edit(int id,[Bind] EmployeeModel employee)
        {
            try
            {
                if (id != employee.EmpId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    empBusiness.UpdateEmployeeById(employee);
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = empBusiness.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            empBusiness.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
