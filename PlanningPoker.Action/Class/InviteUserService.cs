using PlanningPoker.Action.Interface;
using PlanningPoker.Common;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Class
{
    public class InviteUserService : IInviteUserService
    {
        private readonly IInviteUserRepository _inviteUserRepository;
        public InviteUserService(IInviteUserRepository inviteUserRepository)
        {
            _inviteUserRepository = inviteUserRepository;
        }

        public PageResult<TeamMemberResponse> GetTeamMembers(Enums.UserAcceptance acceptance, long userid, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _inviteUserRepository.GetTeamMembers(acceptance, userid, pageIndex, pageSize);
            return result;
        }

        public PageResult<InvitedUserListResponse> GetUsersByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _inviteUserRepository.GetUsersByGameId(gameId, pageIndex, pageSize);
            return result;
        }

        public IEnumerable<InviteUser> InviteUsers(IList<InviteUserResponse> model, long gameId, string datetime, long userId)
        {
            var result = _inviteUserRepository.InviteUsers(model, gameId, datetime, userId);
            return result;
        }

        public InviteUser UpdateUser(long gameid, long userId, InviteUserRequest inviteUser)
        {
            var result = _inviteUserRepository.UpdateUser(gameid, userId, inviteUser);
            return result;
        }
    }
}
