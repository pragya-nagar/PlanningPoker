using System;
using System.Collections.Generic;

namespace PlanningPoker.Domain.Entities
{
    public partial class User
    {
        public long UserId { get; set; }
        public int? UserTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public long? SupervisorId { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid RowGuid { get; set; }
    }
}
