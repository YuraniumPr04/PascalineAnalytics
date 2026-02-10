using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly DataManager _dataManager;

        public SubscriptionService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IEnumerable<SubscriptionPlanDTO>> GetAllPlansAsync()
        {
            var entities = await _dataManager.SubscriptionPlans.GetAll().ToListAsync();

            var dtos = entities.Select(e => new SubscriptionPlanDTO
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price,
                DurationDays = e.DurationDays,
                StorageLimitGb = e.StorageLimitGb,
                DailyRequestsLimit = e.DailyRequestsLimit,
                Description = e.Description
            });

            return dtos;
        }

        public async Task<SubscriptionPlanDTO> GetPlanByIdAsync(int id)
        {
            var entity = await _dataManager.SubscriptionPlans.GetByIdAsync(id);

            if (entity == null) return null;

            return new SubscriptionPlanDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                DurationDays = entity.DurationDays,
                StorageLimitGb = entity.StorageLimitGb,
                DailyRequestsLimit = entity.DailyRequestsLimit,
                Description = entity.Description
            };
        }

        public async Task<bool> PurchasePlanAsync(string userId, int planId)
        {
            var plan = await _dataManager.SubscriptionPlans.GetByIdAsync(planId);
            if (plan == null) return false;


            bool alreadyHasThisPlan = await _dataManager.UserSubscriptions.GetAll()
                .AnyAsync(s => s.UserId == userId &&
                               s.SubscriptionPlanId == planId &&
                               s.EndDate > DateTime.UtcNow);

            if (alreadyHasThisPlan) return false;

            // Підписка діє: (дні плану) + (залишок сьогоднішнього дня)
            DateTime now = DateTime.UtcNow;

            // Початок: зараз
            DateTime startDate = now;

            // Кінець: сьогодні + Duration + встановлення часу на 23:59:59
            DateTime endDate = now.Date
                .AddDays(plan.DurationDays)
                .AddHours(23).AddMinutes(59).AddSeconds(59);

            var newSubscription = new UserSubscription
            {
                UserId = userId,
                SubscriptionPlanId = planId,
                PurchaseDate = now,
                StartDate = startDate,
                EndDate = endDate,
                PricePaid = plan.Price
            };

            await _dataManager.UserSubscriptions.CreateAsync(newSubscription);
            return true;
        }

        public async Task<UserSubscription> GetActiveSubscriptionAsync(string userId)
        {

            return await _dataManager.UserSubscriptions.GetAll()
                .Where(s => s.UserId == userId && s.EndDate > DateTime.UtcNow)
                .OrderByDescending(s => s.EndDate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasActiveSubscriptionAsync(string userId)
        {

            return await _dataManager.UserSubscriptions.GetAll()
                .AnyAsync(s => s.UserId == userId && s.EndDate > DateTime.UtcNow);
        }

        public async Task CreatePlanAsync(SubscriptionPlanDTO planDto)
        {
            if (planDto == null)
            {
                throw new ArgumentNullException(nameof(planDto), "Дані плану не можуть бути пустими");
            }


            var newPlan = new SubscriptionPlan
            {
                Name = planDto.Name,
                Price = planDto.Price,
                DurationDays = planDto.DurationDays,
                StorageLimitGb = planDto.StorageLimitGb,
                DailyRequestsLimit = planDto.DailyRequestsLimit,
                Description = planDto.Description
            };


            await _dataManager.SubscriptionPlans.CreateAsync(newPlan);
        }

        public async Task UpdatePlanAsync(SubscriptionPlanDTO planDto)
        {
            if (planDto == null) throw new ArgumentNullException(nameof(planDto));

            var plan = await _dataManager.SubscriptionPlans.GetByIdAsync(planDto.Id);

            if (plan == null)
            {
                throw new Exception($"План з ID {planDto.Id} не знайдено.");
            }

            plan.Name = planDto.Name;
            plan.Description = planDto.Description;
            plan.DurationDays = planDto.DurationDays;
            plan.StorageLimitGb = planDto.StorageLimitGb;
            plan.DailyRequestsLimit = planDto.DailyRequestsLimit;
            plan.Price = planDto.Price;

            await _dataManager.SubscriptionPlans.UpdateAsync(plan);
        }

        public async Task DeletePlanAsync(int id)
        {
            await _dataManager.SubscriptionPlans.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserSubscriptionDTO>> GetAllUserSubscriptionsAsync()
        {
            var query = _dataManager.UserSubscriptions.GetAll();

            var subscriptions = await query.ToListAsync();


            var subscriptionDtos = subscriptions.Select(s => new UserSubscriptionDTO
            {
                Id = s.Id,
                UserId = s.UserId,
                SubscriptionPlanId = s.SubscriptionPlanId,
                PurchaseDate = s.PurchaseDate,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                PricePaid = s.PricePaid,
                IsAutoRenewEnabled = s.IsAutoRenewEnabled
            }).ToList();

            return subscriptionDtos;
        }
    }
}