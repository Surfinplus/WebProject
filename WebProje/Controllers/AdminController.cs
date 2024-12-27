using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
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

        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
