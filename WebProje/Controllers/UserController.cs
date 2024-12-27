using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    public IActionResult UserDashboard()
    {
        return View();
    }
} 