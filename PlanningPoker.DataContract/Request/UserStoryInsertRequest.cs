using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Request
{
    public class UserStoryInsertRequest
    {
        public long GameId { get; set; }
        public long UserStoryId { get; set; }
        public string Story { get; set; }
        public string Description { get; set; }
    }
}
