using BusinessLayer;
using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructures.Extensions;
using WebApp.Models.Subscription;

namespace WebApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private ServiceManager _serviceManager;
        public SubscriptionController(ServiceManager serviceManager)
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

            var currentUser = HttpContext.Session.GetJson<UserDTO>("CurrentUser");
            if (currentUser != null)
            {
                var activeSub = await _serviceManager.Subscriptions.GetActiveSubscriptionAsync(currentUser.Id);

                if (activeSub != null)
                {
                    model.CurrentSubscriptionPlan = viewModels.FirstOrDefault(p => p.Id == activeSub.SubscriptionPlanId);
                }
            }

            return View(model);
        }

        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyConfirmed(string userId, int id)
        {
            await _serviceManager.Subscriptions.PurchasePlanAsync(userId, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
