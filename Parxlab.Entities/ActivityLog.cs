using System;
using Parxlab.Entities.Enums;
using Parxlab.Entities.Identity;

namespace Parxlab.Entities
{
   public class ActivityLog:BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ActivityType Type { get; set; }
        public string Identity { get; set; }
    }
}
