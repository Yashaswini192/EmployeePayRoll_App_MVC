using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IEmpRepo
    {
        public string AddEmployee(EmployeeModel employeeModel);

        public IEnumerable<EmployeeModel> GetAllEmployees();

        public EmployeeModel UpdateEmployeeById(EmployeeModel employeeModel);

        public EmployeeModel GetEmployeeById(int emp_Id);
        
        public string DeleteEmployee(int Id);

    }
}
