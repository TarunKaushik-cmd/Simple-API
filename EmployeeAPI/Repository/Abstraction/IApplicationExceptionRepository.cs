using EmployeeAPI.Models;
using System;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository.Abstraction
{
    public interface IApplicationExceptionRepository
    {
        Task<int> PostApplicationException(Exception e);
        Task<ExceptionRecords> GetApplicationError(int id);
    }
}
