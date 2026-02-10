using BusinessLayer.DTOs;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionPlanDTO>> GetAllPlansAsync();
        Task<SubscriptionPlanDTO> GetPlanByIdAsync(int id);
        Task UpdatePlanAsync(SubscriptionPlanDTO planDto);
        Task CreatePlanAsync(SubscriptionPlanDTO planDto);
        Task DeletePlanAsync(int id);

        Task<bool> PurchasePlanAsync(string userId, int planId);

        Task<UserSubscription> GetActiveSubscriptionAsync(string userId);
        Task<bool> HasActiveSubscriptionAsync(string userId);

        Task<IEnumerable<UserSubscriptionDTO>> GetAllUserSubscriptionsAsync();
    }
}
