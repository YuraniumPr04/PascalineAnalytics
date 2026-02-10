using DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class _ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(_ApplicationContext).Assembly);
        }

        public _ApplicationContext(DbContextOptions<_ApplicationContext> options)
            : base(options)
        {
            
        }

    }
}

