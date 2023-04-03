using System;

namespace EmployeeAPI.Models
{
    public class ExceptionRecords
    {
        public int Id { get; set; }
        public string ExceptionType { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
