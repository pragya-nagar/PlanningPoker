using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Request
{
    public class InviteUserRequest
    {
        public bool IsAccepted { get; set; }
        public string Reason { get; set; }
    }
}
