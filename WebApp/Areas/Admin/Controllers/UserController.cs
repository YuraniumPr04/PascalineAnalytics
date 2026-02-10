using BusinessLayer;
using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebApp.Areas.Admin.Models.User;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "admin")]
    public class UserController : Controller
    {

        private ServiceManager _serviceManager;
        public UserController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userDtos = await _serviceManager.Users.GetAllUsersAsync();

            var userViewModels = userDtos.Select(u => new UserViewModel
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Telephone = u.Telephone,
                Roles = u.Roles ?? new List<string>()
            }).ToList();

            var model = new IndexViewModel
            {
                UserViewModels = userViewModels
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _serviceManager.Users.GetAllRolesAsync();

            var model = new CreateViewModel
            {
                AllRoles = roles.Select(r => new SelectListItem { Value = r, Text = r }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
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
                        Roles = new List<string> { model.SelectedRole } 
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

                    return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var userDto = await _serviceManager.Users.GetUserByIdAsync(id);
            if (userDto == null) return NotFound();

            var allRoles = await _serviceManager.Users.GetAllRolesAsync();

            var currentRole = userDto.Roles.FirstOrDefault();

            var model = new EditViewModel
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Username = userDto.UserName,
                Telephone = userDto.Telephone,
                SelectedRole = currentRole,
                AllRoles = allRoles.Select(r => new SelectListItem
                {
                    Value = r,
                    Text = r,
                    Selected = r == currentRole
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
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
                        Roles = new List<string> { model.SelectedRole }
                    };

                    await _serviceManager.Users.UpdateUserAsync(userDto);
                    return RedirectToAction(nameof(Index));
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
                    return RedirectToAction(nameof(Index));
                }

                await _serviceManager.Users.DeleteUserAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
