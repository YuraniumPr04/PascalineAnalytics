using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class User : IdentityUser
    {
        public long UsedStorageBytes { get; set; }
        public int QueriesUsed { get; set; }
        public DateTime LastQueriesResetDate { get; set; }  

        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
    }
}
