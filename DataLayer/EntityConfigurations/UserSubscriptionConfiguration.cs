using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations
{
    public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.ToTable("UserSubscriptions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PricePaid)
                .HasPrecision(18, 2);

            builder.HasOne(x => x.User)
                .WithMany(u => u.UserSubscriptions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(x => x.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UserId, x.EndDate });
        }
    }
}