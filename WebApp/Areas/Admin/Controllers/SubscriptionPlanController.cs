using BusinessLayer;
using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApp.Areas.Admin.Models.SubscriptionPlan;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "admin")]
    public class SubscriptionPlanController : Controller
    {
        private ServiceManager _serviceManager;
        public SubscriptionPlanController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dtos = await _serviceManager.Subscriptions.GetAllPlansAsync();

            var viewModels = dtos.Select(dto => new SubscriptionPlanViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                DurationDays = dto.DurationDays,
                Description = dto.Description,
                StorageLimitGb = dto.StorageLimitGb,
                DailyRequestsLimit = dto.DailyRequestsLimit,
                Price = dto.Price
            }).ToList();

            var model = new IndexViewModel
            {
                SubscriptionPlans = viewModels
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _serviceManager.Subscriptions.GetPlanByIdAsync(id);

            if (dto == null)
            {
                return NotFound();
            }

            var model = new EditViewModel
            {
                Id = dto.Id, 
                Name = dto.Name,
                DurationDays = dto.DurationDays,
                Description = dto.Description,
                StorageLimitGb = dto.StorageLimitGb,
                DailyRequestsLimit = dto.DailyRequestsLimit,
                Price = dto.Price
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newPlan = new SubscriptionPlanDTO
                {
                    Name = model.Name,
                    DurationDays = model.DurationDays.Value,
                    Description = model.Description,
                    StorageLimitGb = model.StorageLimitGb.Value,
                    DailyRequestsLimit = model.DailyRequestsLimit.Value,
                    Price = model.Price.Value,
                };

                _serviceManager.Subscriptions.CreatePlanAsync(newPlan);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            try
            {
                
                var dto = new BusinessLayer.DTOs.SubscriptionPlanDTO
                {
                    Id = model.Id, 
                    Name = model.Name,
                    Price = model.Price.Value,
                    DurationDays = model.DurationDays.Value,
                    StorageLimitGb = model.StorageLimitGb.Value,
                    DailyRequestsLimit = model.DailyRequestsLimit.Value,
                    Description = model.Description
                };

                
                await _serviceManager.Subscriptions.UpdatePlanAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Не вдалося зберегти зміни: {ex.Message}");

                return View(model);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceManager.Subscriptions.DeletePlanAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
