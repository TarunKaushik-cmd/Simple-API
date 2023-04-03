using EmployeeAPI.Models;
using EmployeeAPI.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IEnumerable<Department> GetDeptList()
        {
            return _departmentRepository.GetDepartments();
        }
    }
}
