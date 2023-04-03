using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EmployeeAPI.Models
{
    public partial class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public int? DeptId { get; set; }

        public virtual Department Dept { get; set; }
    }
}
