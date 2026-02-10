using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

public class HomeController : Controller
{
    // Сторінка "Головна"
    public IActionResult Index()
    {
        return View();
    }

    // Сторінка "Про нас"
    public IActionResult About()
    {
        return View();
    }

    // Сторінка "Зворотній зв'язок"
    public IActionResult FAQ()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}