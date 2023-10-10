using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Edit(int id, [Bind] EmployeeModel employee)
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
            catch (Exception ex)
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
        [HttpGet]
        [Route("Emp/UserData")]
        public IActionResult Details(int id)
        {
            //id =(int) HttpContext.Session.GetInt32("EmpId");
            if (id == null)
            {
                return NotFound();
            }
            var employee = empBusiness.GetEmployeeById(id);
            ViewBag.Message = "Data Fetched Successfully".ToString();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        [Route("EmpData")]
        public IActionResult EmpDetails(int id)
        {
            id = (int)HttpContext.Session.GetInt32("EmpId");
            if (id == null)
            {
                return NotFound();
            }
            var employee = empBusiness.GetEmployeeById(id);
            ViewBag.Message = "Data Fetched Successfully".ToString();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = empBusiness.Login(model);
                    if (result != null)
                    {
                        HttpContext.Session.SetInt32("EmpId", result.EmpId);
                        HttpContext.Session.SetString("EmpName", result.Name);
                        return RedirectToAction("EmpDetails");
                    }
                    return NotFound();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
    }
}
