using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Domain.Entities
{
    public class UserStoryEstimate
    {
        public long UserStoryEstimateId { get; set; }
        public long UserStoryId { get; set; }
        public long Userid { get; set; }
        public int KeyWordTypeId { get; set; }
        public string KeyWord { get; set; }
        public int BotPoints { get; set; }
        public int UserPoints { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
