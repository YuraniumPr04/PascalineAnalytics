using Microsoft.AspNetCore.Mvc;

namespace PascalineAnalytics.Controllers
{
    public class SubscriptionsController : Controller
    {
        // Сторінка "Підписки"
        public IActionResult Index()
        {
            return View();
        }
    }
}
