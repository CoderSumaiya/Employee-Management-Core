using MvcCore_employeeProject.Models;

namespace MvcCore_employeeProject.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int id);

        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<Employee> UpdateEmployeeAsync(Employee employee);

        Task<IEnumerable<Department>> GetDepartmentsAsync();

        Task<Employee> DeleteEmployeeAsync(int id);


        Task DeleteAcademicByEmployeeIdAsync(int employeeId);

        Task AddAcademicByEmployeeIdAsync(int id, List<AcademicDetail> academics);

        Task<IEnumerable<AcademicDetail>> GetAcademicDetailsByEmployeeIdAsync(int employeeId);

        IQueryable<Employee> GetEmployeeQuery();
    }
}
