using Microsoft.EntityFrameworkCore;
using MvcCore_employeeProject.Data;
using MvcCore_employeeProject.Models;
using System.Reflection;

namespace MvcCore_employeeProject.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee> DeleteEmployeeAsync(int id)
        {
            Employee employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
            return employee;
        }
        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }
        public async Task<IEnumerable<AcademicDetail>> GetAcademicDetailsByEmployeeIdAsync(int employeeId)
        {
            return await _dbContext.AcademicDetails
                   .Where(a => a.EmployeeId == employeeId)
                   .ToListAsync();
        }
        public  async Task AddAcademicByEmployeeIdAsync(int id, List<AcademicDetail> academics)
        {
            if (academics != null)
            {
                foreach (var academic in academics)
                {
                    academic.EmployeeId = id;
                    await _dbContext.AcademicDetails.AddAsync(academic);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAcademicByEmployeeIdAsync(int employeeId)
        {
            var academics = await GetAcademicDetailsByEmployeeIdAsync(employeeId);
            if (academics != null && academics.Any())
            {
                _dbContext.AcademicDetails.RemoveRange(academics);
                await _dbContext.SaveChangesAsync();
            }
        }

             
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {

            return await _dbContext.Employees
            .Include(d => d.Department)
            .Include(a => a.AcademicDetails)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employees = await _dbContext.Employees
               .Include(d => d.Department)
           .Include(a => a.AcademicDetails)
           .ToListAsync();
            return employees;
        }

        

        public IQueryable<Employee> GetEmployeeQuery()
        {
           
            return _dbContext.Employees.AsQueryable();
        }
    }
}
