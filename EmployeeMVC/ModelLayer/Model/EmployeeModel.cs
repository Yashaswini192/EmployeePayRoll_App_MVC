using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        
        [RegularExpression("^[A-Z]{1}[a-z]{4,}",ErrorMessage ="Please Enter A Valid Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} cannot be Empty")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "{0} cannot be Empty")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "{0} cannot be Empty")]
        public string Department { get; set; }

        [Required(ErrorMessage ="{0} cannot be Empty")]
        [RegularExpression("^[0-9]{1,}$",ErrorMessage ="Please Enter A Valid Number")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "{0} cannot be Empty")]
        public DateTime StartDate { get; set; }

        public string Notes { get; set; }
    }
}
