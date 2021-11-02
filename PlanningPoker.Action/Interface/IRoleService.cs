using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using System;
using System.Collections.Generic;
using System.Text;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.Action.Interface
{
    public interface IRoleService
    {
        PageResult<UserResponse> GetAllUsers(Status status, int pageIndex = 0, int pageSize = int.MaxValue);
        PageResult<UserRoleResponse> GetUserByName(int pageIndex = 0, int pageSize = int.MaxValue);
        long UpdateUserRole(IList<UpdateUserRoleRequest> model);
    }
}
