using System;
using System.Collections.Generic;
using System.Text;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.DataContract.Request
{
    public class UpdateUserRoleRequest
    {
        public long UserId { get; set; }
        public RoleName RoleName { get; set; }
    }
}
