using System.Collections.Generic;
using EmployeeAPI.Models;

namespace EmployeeAPI.Repository.Abstraction
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int departmentId);
    }
}
