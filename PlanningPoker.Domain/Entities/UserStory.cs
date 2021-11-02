using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class UserStory
    {
        public long UserStoryId { get; set; }
        public long GameId { get; set; }
        public long? GameSessionId { get; set; }
        public string UserStory1 { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
