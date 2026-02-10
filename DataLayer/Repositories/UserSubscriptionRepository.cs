using DataLayer.Entities;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class UserSubscriptionRepository : IBaseRepository<UserSubscription>
    {
        private readonly _ApplicationContext _context;

        public UserSubscriptionRepository(_ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<UserSubscription> GetAll()
        {
            return _context.UserSubscriptions;
        }

        public async Task<UserSubscription> GetByIdAsync(int id)
        {
            return await _context.UserSubscriptions.FindAsync(id);
        }

        public async Task CreateAsync(UserSubscription item)
        {
            await _context.UserSubscriptions.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSubscription item)
        {
            _context.UserSubscriptions.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.UserSubscriptions.FindAsync(id);
            if (item != null)
            {
                _context.UserSubscriptions.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}