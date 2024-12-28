using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {

        private readonly IEmployeeService _employeeService;

        public IActionResult Login()
        {
            return View();
        }

        public AdminController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Employees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email == "OgrenciNuramarasi@sakarya.edu.tr" && password == "sau")
            {
                return RedirectToAction("AdminPanel");
            }

            TempData["ErrorMessage"] = "Geçersiz admin bilgileri!";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return RedirectToAction("Employees");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Çalışan silinirken hata oluştu: {ex.Message}";
                return RedirectToAction("Employees");
            }
        }


        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
