using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.DataContract.Response
{
    public class UserRoleResponse
    {
        public int RowNum { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string FullName { get; set; }
        public UserRoleIdResponse Role { get; set; }
        public string RoleName { get; set; }
    }
}
