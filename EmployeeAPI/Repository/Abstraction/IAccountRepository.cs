using EmployeeAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository.Abstraction
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(Signup signupModel);
        Task<string> LoginAsync(SignIn signIn);
    }
}
