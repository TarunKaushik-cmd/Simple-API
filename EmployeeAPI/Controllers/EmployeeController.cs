using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using EmployeeAPI.Repository.Abstraction;
using EmployeeAPI.ActionFilter;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using static EmployeeAPI.Repository.Concrete.EmployeeRepository;
using System.Text;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Authorization;
using EmployeeAPI.Repository.Concrete;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IApplicationExceptionRepository _applicationExceptionRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IApplicationExceptionRepository applicationExceptionRepository)
        {
            this.employeeRepository = employeeRepository;
            _applicationExceptionRepository = applicationExceptionRepository;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetEmployeesAsync(int id, string deptname)
        {
            try
            {
                if (id != 0)
                {
                    try
                    {
                        var result = employeeRepository.GetEmployee(id);

                        if (result == null) return NotFound();

                        return Ok(result);
                    }
                    catch (Exception)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error Retrieving Data From The Database");
                    }
                }
                else if (deptname != null)
                {
                    try
                    {
                        var result = employeeRepository.GetEmployeeByDept(deptname);

                        if (result == null) return NotFound();

                        return Ok(result);
                    }
                    catch (Exception)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error Retrieving Data From The Database");
                    }
                }
                else
                {
                    try
                    {
                        var result = employeeRepository.GetEmployees();
                        return Ok(result);
                    }
                    catch (Exception)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error Retrieving Data From The Database");
                    }
                }
            }
            catch (Exception e)
            {
                var errorid = await _applicationExceptionRepository.PostApplicationException(e);
                return Ok("Error Occured. Check Details with Reference Number: " + errorid);
            }
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(EmployeeFilter))]
        public async Task<ActionResult<Employee>> CreateOrUpdateEmployee(GetEmployeesClass employee)
        {
            try
            {
                var createdEmployee = await employeeRepository.AddEmployee(employee);
                if (employee.Employee_Id != 0)
                {
                    return Ok($"Employee Updated Successfully ");
                }
                else
                {
                    return Ok($"Employee Added Successfully ");
                }
            }
            catch (Exception e)
            {
                var errorid = await _applicationExceptionRepository.PostApplicationException(e);
                return Ok("Error Occured. Check Details with Reference Number: " + errorid);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Employee ID");
                }
                else
                {
                    employeeRepository.DeleteEmployee(id);
                }
                return Ok("Deleted Successfully");
            }
            catch (Exception e)
            {
                var errorid = await _applicationExceptionRepository.PostApplicationException(e);
                return Ok("Error Occured. Check Details with Reference Number: " + errorid);
            }
        }
    }
}
