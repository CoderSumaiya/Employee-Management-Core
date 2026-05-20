using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MvcCore_employeeProject.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]

        public string EmployeeName { get; set; }
        [Required, Display(Name = "Joining Date"), DataType(DataType.Date),DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime JoiningDate { get; set; }
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "IsPermanent?")]
        public bool IsPermanent { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [ValidateNever]
        public virtual Department Department { get; set; }

        public virtual ICollection<AcademicDetail> AcademicDetails { get; set; } = new List<AcademicDetail>();
    }
}
