using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlanningPoker.DataContract.Response
{
    public class InviteUserResponse
    {
        public long InvitedUserId { get; set; }
        public string Email { get; set; }
    }
}
