using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MvcCore_employeeProject.Models
{
    public class AcademicDetail
    {
        public int AcademicDetailId { get; set; }

        public int EmployeeId { get; set; }
        public string DegreeName { get; set; }
      
        public decimal CGPA { get; set; }

        [ValidateNever]
        public virtual Employee Employee { get; set; }
    }
}
