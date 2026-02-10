using BusinessLayer;
using BusinessLayer.DTOs;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebApp.Infrastructures.Extensions;
using WebApp.Models.Account;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private ServiceManager _serviceManager;
        public AccountController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userDto = new UserDTO
                    {
                        Email = model.Email,
                        UserName = model.Username,
                        Password = model.Password,
                        Telephone = model.Telephone,
                        Roles = new List<string> { "User" }
                    };

                    await _serviceManager.Users.CreateUserAsync(userDto);

                    var users = await _serviceManager.Users.GetAllUsersAsync();
                    var createdUser = users.FirstOrDefault(u => u.Email == model.Email);

                    if (createdUser != null)
                    {
                        var plans = await _serviceManager.Subscriptions.GetAllPlansAsync();


                        var basicPlan = plans.FirstOrDefault(p => p.Name == "Basic");

                        if (basicPlan != null)
                        {
                            await _serviceManager.Subscriptions.PurchasePlanAsync(createdUser.Id, basicPlan.Id);
                        }
                    }

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var roles = await _serviceManager.Users.GetAllRolesAsync();
            model.AllRoles = roles.Select(r => new SelectListItem { Value = r, Text = r }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userDto = await _serviceManager.Users.AuthenticateAsync(model.Email, model.Password);

            if (userDto != null)
            {
                HttpContext.Session.SetJson("CurrentUser", userDto);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Спроба входу недійсна.");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            // Якщо ID не передано, пробуємо взяти ID поточного юзера з сесії
            if (string.IsNullOrEmpty(id))
            {
                var currentUser = HttpContext.Session.GetJson<UserDTO>("CurrentUser");
                id = currentUser?.Id;
            }

            if (string.IsNullOrEmpty(id)) return NotFound();

            var userDto = await _serviceManager.Users.GetUserByIdAsync(id);
            if (userDto == null) return NotFound();

            var model = new IndexViewModel
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Username = userDto.UserName,
                Telephone = userDto.Telephone,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Захист обов'язковий для Post
        public async Task<IActionResult> Edit(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userDto = new UserDTO
                    {
                        Id = model.Id,
                        Email = model.Email,
                        UserName = model.Username,
                        Telephone = model.Telephone,
                    };

                    await _serviceManager.Users.UpdateUserAsync(userDto);

                    // ОНОВЛЕННЯ СЕСІЇ: якщо користувач редагує сам себе, треба оновити дані в сесії
                    var currentUser = HttpContext.Session.GetJson<UserDTO>("CurrentUser");
                    if (currentUser != null && currentUser.Id == model.Id)
                    {
                        currentUser.Email = model.Email;
                        currentUser.UserName = model.Username;
                        HttpContext.Session.SetJson("CurrentUser", currentUser);
                    }

                    // Перенаправляємо назад на Index з тим самим ID
                    return RedirectToAction(nameof(Index), new { id = model.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Index", model); // Повертаємо View Index, якщо форма не валідна
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (currentUserId == id)
                {
                    return RedirectToAction("Index", "Home");
                }

                await _serviceManager.Users.DeleteUserAsync(id);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }


    }

}
