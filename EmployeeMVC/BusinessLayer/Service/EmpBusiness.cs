using BusinessLayer.Interface;
using ModelLayer.Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class EmpBusiness : IEmpBusiness
    {
        private readonly IEmpRepo empRepo;

        public EmpBusiness(IEmpRepo empRepo)
        {
             this.empRepo = empRepo;
        }

        public string AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                return empRepo.AddEmployee(employeeModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            try
            {
                return empRepo.GetAllEmployees();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
