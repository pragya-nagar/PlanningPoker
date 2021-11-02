using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Response
{
    public class TeamMemberResponse
    {
        public int RowNum { get; set; }
        public long GameId { get; set; }
        public long InviteUserId { get; set; }
        public string GameName { get; set; }
        public long UserStories { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
