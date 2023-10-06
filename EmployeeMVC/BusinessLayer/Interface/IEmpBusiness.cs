using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IEmpBusiness
    {
        public string AddEmployee(EmployeeModel employeeModel);

        public IEnumerable<EmployeeModel> GetAllEmployees();

        public EmployeeModel GetEmployeeById(int emp_Id);

        public EmployeeModel UpdateEmployeeById(EmployeeModel employeeModel);

        public string DeleteEmployee(int Id);

    }
}
