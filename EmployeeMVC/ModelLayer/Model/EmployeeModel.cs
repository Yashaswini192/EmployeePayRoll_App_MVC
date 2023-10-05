using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }

        public string Name { get; set; }

        public string ProfileImage { get; set; }

        public string Gender { get; set; }

        public string Department { get; set; }

        public decimal Salary { get; set; }

        public DateTime StartDate { get; set; }

        public string Notes { get; set; }
    }
}
