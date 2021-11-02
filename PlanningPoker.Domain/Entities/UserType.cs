using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class UserType
    {
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
