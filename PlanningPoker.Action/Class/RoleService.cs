using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.Repository.Interface;
using PlanningPoker.Action.Interface;
using PlanningPoker.DataContract.Response;
using static PlanningPoker.Common.Enums;
using PlanningPoker.DataContract.Request;

namespace PlanningPoker.Action.Class
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public PageResult<UserResponse> GetAllUsers(Status status, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _roleRepository.GetAllUsers(status, pageIndex, pageSize);
            return result;
        }
        public PageResult<UserRoleResponse> GetUserByName(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _roleRepository.GetUserByName(pageIndex, pageSize);
            return result;
        }
        public long UpdateUserRole(IList<UpdateUserRoleRequest> model)
        {
            var result = _roleRepository.UpdateUserRole(model);
            return result;
        }
    }
}
