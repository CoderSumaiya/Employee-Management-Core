namespace MvcCore_employeeProject.Models
{
    public class DepartmentHeadCount
    {
        public int DepartmentId { get; set; }
        public int Count { get; set; }
        public DateTime JoiningDate { get; set; }
        public string DepartmentName { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
