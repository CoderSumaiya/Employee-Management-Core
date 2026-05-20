using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCore_employeeProject.Data;
using MvcCore_employeeProject.Models;

namespace MvcCore_employeeProject.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public HeadCountViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var departmentCount = await _db.Employees
                .Include(e => e.Department) 
                .GroupBy(e => new
                {
                    e.DepartmentId,
                    e.Department.DepartmentName
                })
                .Select(g => new DepartmentHeadCount
                {
                    DepartmentId = g.Key.DepartmentId,
                    DepartmentName = g.Key.DepartmentName,

                 
                    Count = g.Count(),
                    TotalSalary = g.Sum(e => e.Salary),

                  
                    JoiningDate = g.Max(e => e.JoiningDate)
                })
                .ToListAsync();

            return View(departmentCount);
        }
    }
}
