using EmployeeAPI.Repository.Abstraction;
using EmployeeAPI.Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAPI.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly TESTDBContext _dbContext;

        public DepartmentRepository(TESTDBContext appDbContext)
        {
            this._dbContext = appDbContext;
        }

        public Department GetDepartment(int departmentId)
        {
            return _dbContext.Departments
                .FirstOrDefault(d => d.DeptId == departmentId);
        }
        //public List<Department> GetDepartments()
        //{
        //    var depts= _dbContext.Departments.Select(d => d.DeptName).ToList();
        //    return
        //}
        public IEnumerable<Department> GetDepartments()
        {
            return _dbContext.Departments;
        }
    }
}
