using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class InviteUser
    {
        public long InviteUserId { get; set; }
        public long GameId { get; set; }
        public long? GameSessionId { get; set; }
        public long UserId { get; set; }
        public string Email { get; set; }
        public bool? IsAccepted { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
