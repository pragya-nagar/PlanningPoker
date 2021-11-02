using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
