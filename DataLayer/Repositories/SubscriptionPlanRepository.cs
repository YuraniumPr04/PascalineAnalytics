using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class SubscriptionPlanRepository : IBaseRepository<SubscriptionPlan>
    {
        private readonly _ApplicationContext _context;

        public SubscriptionPlanRepository(_ApplicationContext context)
        {
            _context = context;
        }


        public IQueryable<SubscriptionPlan> GetAll()
        {
            return _context.SubscriptionPlans;
        }

        public async Task<SubscriptionPlan> GetByIdAsync(int id)
        {
            return await _context.SubscriptionPlans.FindAsync(id);
        }

        public async Task CreateAsync(SubscriptionPlan item)
        {
            await _context.SubscriptionPlans.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubscriptionPlan item)
        {
            _context.SubscriptionPlans.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.SubscriptionPlans.FindAsync(id);
            if (plan != null)
            {
                _context.SubscriptionPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }
    }
}