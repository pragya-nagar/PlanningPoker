using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.Repository.Class
{
    public class InviteUserRepository : IInviteUserRepository
    {
        private readonly AppDBContext _dataContext;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IEmailRepository _emailRepository;
        public InviteUserRepository(AppDBContext dataContext, IUserRepository userRepository, IGameRepository gameRepository, IEmailRepository emailRepository)
        {
            _dataContext = dataContext;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
            _emailRepository = emailRepository;
        }
        public IEnumerable<InviteUser> InviteUsers(IList<InviteUserResponse> model, long gameId, string datetime, long userId)
        {
            var gameSession = _dataContext.GameSession.Where(x => x.GameId == gameId).OrderByDescending(x => x.GameSessionId).FirstOrDefault();
            gameSession.SessionTime = Convert.ToDateTime(datetime);
            gameSession.UpdatedOn = DateTime.Now;
            _dataContext.GameSession.Update(gameSession).Property(x => x.GameSessionId).IsModified = false;
            _dataContext.SaveChanges();

            var inviteUsersList = new List<InviteUser>();
            foreach (var c in model)
            {
                var user = new InviteUser();
                user.Email = c.Email;
                user.UserId = c.InvitedUserId;
                var query = _dataContext.Game.FirstOrDefault(x => x.GameId == gameId);
                if (query != null)
                {
                    var name = _userRepository.GetById(user.UserId);
                    var email = new Email();
                    email.FromEmail = "botcardadmin@compunnel.net";
                    email.FromName = "BOTCard";
                    email.ToEmail = user.Email;
                    email.ToName = name.FirstName;
                    email.Subject = "Invitation For estimation";
                    email.EmailText = "Hi, Please join the Estimation Process on" + gameSession.SessionTime.ToString("MM/dd/yyyy HH:mm");
                    email.CreatedBy = userId;
                    email.UpdatedBy = userId;
                    _emailRepository.CreateEmail(email);

                    var sendMail = SendEmail(user.Email, "botcardadmin@compunnel.net", "Invitation For estimation", "Hi, Please join the Estimation Process on" + gameSession.SessionTime.ToString("MM/dd/yyyy HH:mm"));
                }
                user.GameId = gameId;
                user.GameSessionId = gameSession.GameSessionId;
                user.CreatedOn = DateTime.Now;
                user.UpdatedOn = DateTime.Now;
                user.CreatedBy = userId;
                user.UpdatedBy = userId;
                user.RowGuid = Guid.NewGuid();
                _dataContext.InviteUser.Add(user);
            }
            _dataContext.SaveChanges();
            return inviteUsersList;
        }

        private string SendEmail(string to, string from, string subject, string content)
        {
            try
            {
                MimeMessage message = new MimeMessage()
                {
                    Subject = subject,
                    Body = new TextPart("Plain") { Text = content }
                };
                message.From.Add(new MailboxAddress(from));
                message.To.Add(new MailboxAddress(to.ToString()));

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(
                        "smtp.gmail.com",
                        587,
                        SecureSocketOptions.StartTls
                    );

                    smtp.Authenticate("botcardadmin@compunnel.net", "Bot Card@5862");
                    smtp.Send(message);
                    smtp.Disconnect(true);

                }
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Failed. Error: {ex.Message}";
            }
        }

        public PageResult<InvitedUserListResponse> GetUsersByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var users = new List<InvitedUserListResponse>();
            var query = from sc in _dataContext.InviteUser.Where(x => x.GameId == gameId)
                        join tc in _dataContext.User on sc.UserId equals tc.UserId
                        join uc in _dataContext.UserSession on tc.UserId equals uc.UserId
                        select (new InvitedUserListResponse
                        {
                            Id = sc.InviteUserId,
                            GameId = sc.GameId,
                            UserId = sc.UserId,
                            FirstName = tc.FirstName,
                            LastName = tc.LastName,
                            Email = sc.Email,
                            IsAccepted = sc.IsAccepted,
                            Reason = sc.Reason,
                            IsActive = uc.IsActive
                        });

            var skipAmount = pageSize * pageIndex;
            var totalRecords = query.Count();
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;


            var result = new PageResult<InvitedUserListResponse>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;
            var PagedQuery = query.OrderBy(x => x.Id).Skip(skipAmount).Take(pageSize).ToList();
            if (PagedQuery.Count > 0)
            {

                foreach (var user in PagedQuery)
                {
                    users.Add(user);
                }
            }
            result.Results = users;
            return result;
        }
        public InviteUser UpdateUser(long gameid, long userId, InviteUserRequest inviteUser)
        {
            var query = _dataContext.InviteUser.FirstOrDefault(x => x.GameId == gameid && x.UserId == userId);
            var createdBy = _gameRepository.GetById(gameid);
            var email = _userRepository.GetById(createdBy.CreatedBy);

            query.IsAccepted = inviteUser.IsAccepted;
            query.Reason = inviteUser.Reason;
            query.UpdatedOn = DateTime.Now;
            _dataContext.InviteUser.Update(query).Property(x => x.InviteUserId).IsModified = false; ;
            _dataContext.SaveChanges();

            var emailList = new Email();
            emailList.FromEmail = "botcardadmin@compunnel.net";
            emailList.FromName = "BOTCard";
            emailList.ToEmail = email.Email;
            emailList.ToName = email.FirstName;
            emailList.Subject = "Invited User's Response for estimation";
            emailList.CreatedBy = userId;
            emailList.UpdatedBy = userId;

            if (inviteUser.IsAccepted == true)
            {        
                emailList.EmailText = "Hi, User " + query.Email + " has Accepted the invitation for game " + createdBy.GameName;  
                _emailRepository.CreateEmail(emailList);

                var sendMail = SendEmail(email.Email, "botcardadmin@compunnel.net", "Invited User's Response for estimation", "Hi, User " + query.Email + " has Accepted the invitation for game " + createdBy.GameName);
            }
            else
            {
                emailList.EmailText = "Hi, User " + query.Email + " has Rejected the invitation for game " + createdBy.GameName + " with reason " + query.Reason;
                _emailRepository.CreateEmail(emailList);

                var sendMail = SendEmail(email.Email, "botcardadmin@compunnel.net", "Invited User's Response for estimation", "Hi, User " + query.Email + " has Rejected the invitation for game " + createdBy.GameName + " with reason " + query.Reason);
            }
            return query;
        }
        public PageResult<TeamMemberResponse> GetTeamMembers(UserAcceptance acceptance, long userid, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            List<InviteUser> uquery;
            if (acceptance == 0)
            {
                uquery = _dataContext.InviteUser.Where(x => x.UserId == userid && ((x.IsAccepted == false) || (x.IsAccepted == true))).ToList();
            }
            else
            {
                uquery = _dataContext.InviteUser.Where(x => x.UserId == userid && (x.IsAccepted == null)).ToList();
            }

            var skipAmount = pageSize * pageIndex;
            var totalRecords = uquery.Count;
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;

            var result = new PageResult<TeamMemberResponse>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;

            int startIndex = (pageIndex * pageSize) + 1;
            var PagedQuery = uquery.OrderBy(x => x.GameId).Skip(skipAmount).Take(pageSize).ToList();

            result.Results = from isAccepted in PagedQuery
                             from gameName in _dataContext.Game.Where(y => y.GameId == isAccepted.GameId).ToList()
                             let userStories = _dataContext.UserStory.Where(x => x.GameId == isAccepted.GameId).Count()

                             select new TeamMemberResponse
                             {
                                 RowNum = startIndex++,
                                 GameName = gameName.GameName,
                                 UserStories = userStories,
                                 GameId = isAccepted.GameId,
                                 InviteUserId = isAccepted.InviteUserId,
                                 UpdatedOn = isAccepted.UpdatedOn,
                                 IsAccepted = isAccepted.IsAccepted

                             };
            return result;
        }
    }
}
