using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.Repository.Interface
{
    public interface IInviteUserRepository
    {
        IEnumerable<InviteUser> InviteUsers(IList<InviteUserResponse> model, long gameId, string datetime, long userId);
        PageResult<InvitedUserListResponse> GetUsersByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue);
        PageResult<TeamMemberResponse> GetTeamMembers(UserAcceptance acceptance, long userid, int pageIndex = 0, int pageSize = int.MaxValue);
        InviteUser UpdateUser(long gameid, long userId, InviteUserRequest inviteUser);
    }
}
