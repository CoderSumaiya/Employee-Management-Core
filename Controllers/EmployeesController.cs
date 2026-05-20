using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCore_employeeProject.Data;
using MvcCore_employeeProject.Models;
using MvcCore_employeeProject.Models.ViewModels;
using MvcCore_employeeProject.Repositories;

namespace MvcCore_employeeProject.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly IEmployeeRepository _repo;
        private readonly IWebHostEnvironment _env;
        public EmployeesController(IEmployeeRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? departmentId, string searchIsPermanent, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DeptSortParm"] = sortOrder == "Department" ? "dept_desc" : "Department";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";

       
            if (searchString != null || departmentId != null || searchIsPermanent != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["SelectedDept"] = departmentId;
            ViewData["SelectedEnrolled"] = searchIsPermanent;

      
            var employeeQuery = _repo.GetEmployeeQuery()
                .Include(d => d.Department)
                .Include(a => a.AcademicDetails)
                .AsNoTracking();

    
            if (!String.IsNullOrEmpty(searchString))
            {
                employeeQuery = employeeQuery.Where(e => e.EmployeeName.Contains(searchString));
            }

            if (departmentId.HasValue)
            {
                employeeQuery = employeeQuery.Where(e => e.DepartmentId == departmentId.Value);
            }


            if (!String.IsNullOrEmpty(searchIsPermanent))
            {
                bool isPermanent = searchIsPermanent == "true";
                employeeQuery = employeeQuery.Where(e => e.IsPermanent == isPermanent);
            }

            // ViewData teo 'searchIsPermanent' set koro
            ViewData["PermanentFilter"] = searchIsPermanent;

            employeeQuery = sortOrder switch
            {
                "name_desc" => employeeQuery.OrderByDescending(e => e.EmployeeName),
                "Department" => employeeQuery.OrderBy(e => e.Department.DepartmentName),
                "dept_desc" => employeeQuery.OrderByDescending(e => e.Department.DepartmentName),
                "Status" => employeeQuery.OrderBy(e => e.IsPermanent),
                "status_desc" => employeeQuery.OrderByDescending(e => e.IsPermanent),
                _ => employeeQuery.OrderBy(e => e.EmployeeName), // Default Sort
            };

 
            int pageSize = 3;
            int pageNo = pageNumber ?? 1;

      
            ViewBag.Departments = await _repo.GetDepartmentsAsync();

            var result = await PaginatedList<Employee>.CreateAsync(employeeQuery, pageNo, pageSize);

            return View(result);
        }


        [HttpGet]
        public async Task<JsonResult> CheckMobileExists(string MobileNo, int? EmployeeId)
        {
          
            var employees = await _repo.GetEmployeesAsync();
            bool exists = employees.Any(e => e.MobileNo == MobileNo && e.EmployeeId != EmployeeId);

            if (exists)
            {
                return Json("Mobile number is already in use.");
            }

            return Json(true);
        }
       

     

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreatePartial()
        {
            EmployeeViewModel employee = new EmployeeViewModel
            {
                AcademicDetails = new List<AcademicDetail>(),
                Departments = (await _repo.GetDepartmentsAsync()).ToList()
            };

            return View("_CreateEmployeePartial",employee);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = (await _repo.GetDepartmentsAsync()).ToList();

                return PartialView("_CreateEmployeePartial", model); 
            }

            Employee employee = new Employee
            {
                EmployeeName = model.EmployeeName,
                JoiningDate = model.JoiningDate,
                Salary = model.Salary,
                MobileNo = model.MobileNo,
                IsPermanent = model.IsPermanent,
                DepartmentId = model.DepartmentId,
                AcademicDetails = model.AcademicDetails ?? new List<AcademicDetail>()
            };

            if (model.ProfileFile != null)
            {
                string imageFileName = await GetImageFileNameAsync(model.ProfileFile);
                employee.ImageUrl = "~/images/" + imageFileName;
            }
            else
            {
                employee.ImageUrl = "~/images/noimage.png";
            }

            await _repo.AddEmployeeAsync(employee);

            return Json(new { success = true }); 
        }
        private async Task<string> GetImageFileNameAsync(IFormFile profileFile)
        {
            if (profileFile == null) return string.Empty;

            string name = Guid.NewGuid().ToString() + "-" + Path.GetFileName(profileFile.FileName);
            var uploadFolder = Path.Combine(_env.WebRootPath, "images");

            if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

            var filePath = Path.Combine(uploadFolder, name);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profileFile.CopyToAsync(fileStream);
            }
            return name;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<JsonResult> DeleteEmployee(int id)
        {
            var invoice = await _repo.GetEmployeeByIdAsync(id);
            if (invoice != null)
            {
                await _repo.DeleteAcademicByEmployeeIdAsync(id);
                await _repo.DeleteEmployeeAsync(id);

                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Employee  not found." });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
       
        public async Task<IActionResult> EditPartial(int id)
        {
            var employee = await _repo.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                JoiningDate = employee.JoiningDate,
                Salary=employee.Salary,
                ImageUrl = employee.ImageUrl,
                MobileNo = employee.MobileNo,
                IsPermanent= employee.IsPermanent,
                DepartmentId = employee.DepartmentId,
                AcademicDetails = employee.AcademicDetails?.ToList() ?? new List<AcademicDetail>(),
                Departments = (await _repo.GetDepartmentsAsync()).ToList()
            };
        return View("_EditEmployeePartial", viewModel);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel model, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
      
                model.Departments = (await _repo.GetDepartmentsAsync()).ToList();
                return PartialView("_EditEmployeePartial", model);
            }

            var existingEmployee = await _repo.GetEmployeeByIdAsync(model.EmployeeId);
            if (existingEmployee == null)
            {
                return Json(new { success = false, message = "Employee not found." });
            }

       
            if (model.ProfileFile != null)
            {
                if (!string.IsNullOrEmpty(existingEmployee.ImageUrl))
                {
                    string fileName = existingEmployee.ImageUrl.TrimStart('~').TrimStart('/');
                    string imagePath = Path.Combine(_env.WebRootPath, fileName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                string imageFileName = await GetImageFileNameAsync(model.ProfileFile);
                existingEmployee.ImageUrl = "~/images/" + imageFileName;
            }
            else
            {
                existingEmployee.ImageUrl = OldImageUrl;
            }

     
            existingEmployee.EmployeeName = model.EmployeeName;
            existingEmployee.JoiningDate = model.JoiningDate;
            existingEmployee.Salary = model.Salary;
            existingEmployee.MobileNo = model.MobileNo;
            existingEmployee.IsPermanent = model.IsPermanent;
            existingEmployee.DepartmentId = model.DepartmentId;

          
            await _repo.UpdateEmployeeAsync(existingEmployee);
            await _repo.DeleteAcademicByEmployeeIdAsync(existingEmployee.EmployeeId);
            await _repo.AddAcademicByEmployeeIdAsync(existingEmployee.EmployeeId, model.AcademicDetails.ToList());

        
            return Json(new { success = true });
        }


    }
}

