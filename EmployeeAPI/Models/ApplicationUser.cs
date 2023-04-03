using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Key]
        public new int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
