using Microsoft.AspNetCore.Mvc;
using MvcCore_employeeProject.Data;

namespace MvcCore_employeeProject.Controllers
{
    public class HomeController : Controller
    {
 
        public IActionResult Index()
        {
            return View();
        }
    }
}
