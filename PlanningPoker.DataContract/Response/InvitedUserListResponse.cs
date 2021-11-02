using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Response
{
    public class InvitedUserListResponse
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public long? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsAccepted { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}
