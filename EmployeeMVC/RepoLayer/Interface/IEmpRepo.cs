﻿using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IEmpRepo
    {
        public string AddEmployee(EmployeeModel employeeModel);

    }
}