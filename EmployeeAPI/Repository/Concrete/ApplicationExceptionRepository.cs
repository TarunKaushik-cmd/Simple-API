using EmployeeAPI.Models;
using EmployeeAPI.Repository.Abstraction;
using EmployeeAPI.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository.Concrete
{
    public class ApplicationExceptionRepository: IApplicationExceptionRepository
    {
        private readonly TESTDBContext _dbContext;
        public ApplicationExceptionRepository(TESTDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> PostApplicationException(Exception e)
        {
            ExceptionRecords applicationException = new ExceptionRecords()
            {
                ExceptionType = e.GetType().ToString(),
                InnerException = Convert.ToString(e.InnerException),
                Message = e.Message,
                CreatedDate = DateTime.Now
            };
            var result =await _dbContext.AddAsync(applicationException);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<ExceptionRecords> GetApplicationError(int id)
        {
            var result = await _dbContext.ExceptionRecordss.FirstOrDefaultAsync(e=>e.Id==id);
            return result;
        }
    }
}
