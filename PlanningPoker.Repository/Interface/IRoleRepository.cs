using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.Repository.Interface
{
    public interface IRoleRepository
    {
        PageResult<UserResponse> GetAllUsers(Status status, int pageIndex = 0, int pageSize = int.MaxValue);
        PageResult<UserRoleResponse> GetUserByName(int pageIndex = 0, int pageSize = int.MaxValue);
        long UpdateUserRole(IList<UpdateUserRoleRequest> model);
    }
}
