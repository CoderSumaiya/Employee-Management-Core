using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MvcCore_employeeProject.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee  name is required")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Joining date cannot be a future date")]
        [Display(Name = "Joining Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime JoiningDate { get; set; } = DateTime.Now;

        [Remote("CheckMobileExists", "Employees", AdditionalFields = "EmployeeId", ErrorMessage = "Mobile number already exists!")]
        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Please enter a valid mobile number (10-11 digits)")]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        
        [Required]
        [Range(0, 999999.99, ErrorMessage = "Salary fee must be a positive value")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
        [Display(Name = "IsPermanent?")]
        public bool IsPermanent { get; set; }


        [Required(ErrorMessage = "Please select a Department")]


        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [ValidateNever]
        public virtual Department Department { get; set; }
        [Display(Name = " Image")]

        [ValidateNever]
        public string? ImageUrl { get; set; }

        [ValidateNever]
        [Display(Name = "Profile Image")]
        [DataType(DataType.Upload)]
        public IFormFile? ProfileFile { get; set; }



        public virtual IList<AcademicDetail> AcademicDetails { get; set; } = new List<AcademicDetail>();
        [ValidateNever]
        public virtual IList<Department> Departments { get; set; }
        [ValidateNever]
        public virtual IList<Employee> Employees { get; set; }
    }
}

