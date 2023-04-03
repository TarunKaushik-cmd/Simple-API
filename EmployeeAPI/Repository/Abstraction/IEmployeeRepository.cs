using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Models;
using static EmployeeAPI.Repository.Concrete.EmployeeRepository;

namespace EmployeeAPI.Repository.Abstraction
{
    public interface IEmployeeRepository
    {
        List<GetEmployeesClass> GetEmployees();
        List<GetEmployeesClass> GetEmployeeByDept(string deptname);
        GetEmployeesClass GetEmployee(int employeeId);
        Task<Employee> AddEmployee(GetEmployeesClass employee);
        Task<Employee> UpdateEmployee(GetEmployeesClass employee);
        void DeleteEmployee(int employeeId);
    }
}
