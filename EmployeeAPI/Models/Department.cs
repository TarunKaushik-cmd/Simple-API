using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EmployeeAPI.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
