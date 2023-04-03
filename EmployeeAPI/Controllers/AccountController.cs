using EmployeeAPI.Models;
using EmployeeAPI.Repository.Abstraction;
using EmployeeAPI.Repository.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationExceptionRepository _applicationExceptionRepository;
        public AccountController(IAccountRepository accountRepository, IApplicationExceptionRepository applicationExceptionRepository)
        {
            _accountRepository = accountRepository;
            _applicationExceptionRepository = applicationExceptionRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] Signup signupModel)
        {
            try
            {
                var result = await _accountRepository.SignUpAsync(signupModel);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                var errorid = await _applicationExceptionRepository.PostApplicationException(e);
                return Ok("Error Occured. Check Details with Reference Number: " + errorid);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignIn signupModel)
        {
            try
            {
                var result = await _accountRepository.LoginAsync(signupModel);
                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized();
                }
                return Ok(result);
               
            }
            catch(Exception e)
            {
                var errorid=await _applicationExceptionRepository.PostApplicationException(e);
                return Ok("Error Occured. Check Details with Reference Number: "+errorid);
            }
        }
    }
}
