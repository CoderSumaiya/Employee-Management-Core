namespace MvcCore_employeeProject.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }


        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
