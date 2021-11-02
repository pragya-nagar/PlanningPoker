using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Request
{
    public class UserUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
