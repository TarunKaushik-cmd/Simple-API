using System.Runtime.Serialization;

namespace EmployeeAPI.Models
{
    [DataContract(Name ="EmployeeModel",Namespace ="")]
    public class GetEmployeesClass // for storing the result of the join
    {
        [DataMember(Order =0)]
        public int Employee_Id { get; set; }
        [DataMember(Order =1)]
        public string? Name { get; set; }
        [DataMember(Order =2)]
        public int? Age { get; set; }
        [DataMember(Order =3)]
        public string? Qualification { get; set; }
        [DataMember(Order =4)]  
        public string? Address { get; set; }
        [DataMember(Order =5)]
        public string Department { get; set; }
    }
}
