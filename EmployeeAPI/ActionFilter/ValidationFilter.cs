using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace EmployeeAPI.ActionFilter
{
    public class EmployeeFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            GetEmployeesClass employee= (GetEmployeesClass)context.ActionArguments["employee"];
            if (!(employee.Age >= 18 && employee.Age < 40))
            {
                context.Result = new BadRequestObjectResult("Not Eligible to Work");
                return;
            }
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

    }
}
