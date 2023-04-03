using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.Abstraction;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using EmployeeAPI.Repository.Context;

namespace EmployeeAPI.Repository.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TESTDBContext _dBContext;

        public EmployeeRepository(TESTDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public List<GetEmployeesClass> GetEmployees()
        {
            var res = _dBContext.Employees.Join(_dBContext.Departments, e => e.DeptId, t => t.DeptId, (e, t) => new GetEmployeesClass
            {
                Employee_Id = e.EmpId,
                Name=e.Name,
                Qualification=e.Qualification,
                Age = e.Age,
                Address = e.Address,
                Department = t.DeptName,
            }).ToList();
            return res;

        }

        public GetEmployeesClass GetEmployee(int employeeId)
        {
            var employee = _dBContext.Employees.Where(qz => qz.EmpId == employeeId).Join(_dBContext.Departments, e => e.DeptId, t => t.DeptId, (e, t) => new GetEmployeesClass
            {
                Employee_Id = e.EmpId,
                Name = e.Name,
                Qualification = e.Qualification,
                Age = e.Age,
                Address = e.Address,
                Department = t.DeptName,
            }).Single();
            return employee;
        }
        public List<GetEmployeesClass> GetEmployeeByDept(string deptname)
        {
            var res = _dBContext.Employees.Where(qz => qz.DeptId == _dBContext.Departments.Where(dept => dept.DeptName == deptname).Select(emp => emp.DeptId).Single()).Join(_dBContext.Departments, e => e.DeptId, t => t.DeptId, (e, t) => new GetEmployeesClass
            {
                Employee_Id = e.EmpId,
                Name = e.Name,
                Qualification = e.Qualification,
                Age = e.Age,
                Address = e.Address,
                Department = t.DeptName,
            }).ToList();
            return res;
        }

        public async Task<Employee> AddEmployee(GetEmployeesClass _employee)
        {
          
            if (_employee.Employee_Id == 0)
            {
                Employee employee1 = new()
                {
                    Name = _employee.Name,
                    Age = _employee.Age,
                    Qualification = _employee.Qualification,
                    Address = _employee.Address,
                    DeptId = _dBContext.Departments.Where(dept => dept.DeptName == _employee.Department).Select(emp => emp.DeptId).Single()
                };
                var result = await _dBContext.Employees.AddAsync(employee1);
                await _dBContext.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                return await UpdateEmployee(_employee);
            }
        }

        public async Task<Employee> UpdateEmployee(GetEmployeesClass employee)
        {
            var result = await _dBContext.Employees
                .FirstOrDefaultAsync(e => e.EmpId == employee.Employee_Id);

            if (result != null)
            {
                result.Name = employee.Name
                    ?? _dBContext.Employees.Where(emp => emp.EmpId == employee.Employee_Id).Select(emp => emp.Name).Single();
                result.Age = employee.Age 
                    ?? _dBContext.Employees.Where(emp => emp.EmpId == employee.Employee_Id).Select(emp => emp.Age).Single(); ;
                result.Address = employee.Address
                    ?? _dBContext.Employees.Where(emp => emp.EmpId == employee.Employee_Id).Select(emp => emp.Address).Single(); ;
                result.Qualification = employee.Qualification 
                    ?? _dBContext.Employees.Where(emp => emp.EmpId == employee.Employee_Id).Select(emp => emp.Qualification).Single(); ;
                result.DeptId = _dBContext.Departments.Where(dept => dept.DeptName == employee.Department).Select(emp => emp.DeptId).Single();

                await _dBContext.SaveChangesAsync();
                return result;
                }

            return null;
        }

        public void DeleteEmployee(int employeeId)
        {
            var result = _dBContext.Employees.FirstOrDefault(e => e.EmpId == employeeId);
            if (result != null)
            {
                _dBContext.Employees.Remove(result);
                _dBContext.SaveChangesAsync();
            }
        }
    }
}
