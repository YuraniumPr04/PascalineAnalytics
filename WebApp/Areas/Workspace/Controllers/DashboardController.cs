using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Workspace.Controllers
{
    [Area("Workspace")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
