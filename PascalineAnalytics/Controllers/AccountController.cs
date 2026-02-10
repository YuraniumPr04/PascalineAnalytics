using Microsoft.AspNetCore.Mvc;

namespace PascalineAnalytics.Controllers
{
    public class AccountController : Controller
    {
        // Сторінка "Вхід"
        public IActionResult Login()
        {
            return View();
        }

        // Сторінка "Профіль" (для увійшовшого користувача)
        public IActionResult Profile()
        {
            return View();
        }
    }
}
