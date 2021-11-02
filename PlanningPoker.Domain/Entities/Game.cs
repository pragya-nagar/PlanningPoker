using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class Game
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public bool? IsChangeVote { get; set; }
        public bool? IsDefinitionOfEstimation { get; set; }
        public bool? IsStoryTimer { get; set; }
        public bool? IsBot { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
