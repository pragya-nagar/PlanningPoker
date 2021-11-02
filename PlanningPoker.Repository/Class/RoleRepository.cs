using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Common;
using PlanningPoker.Repository.Interface;
using static PlanningPoker.Common.Enums;
using NETCore.MailKit.Core;
using PlanningPoker.DataContract.Request;

namespace PlanningPoker.Repository.Class
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDBContext _appDbContext;
        private readonly IEmailSenderRepository _emailSenderRepository;
        public RoleRepository(AppDBContext appDbContext, IEmailSenderRepository emailSenderRepository)
        {
            _appDbContext = appDbContext;
            _emailSenderRepository = emailSenderRepository;
        }

        public PageResult<UserResponse> GetAllUsers(Status status, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            IQueryable<User> query;
            if (status == 0)
            {
                query = _appDbContext.User.Where(x => x.RoleId == 1);
            }
            else
            {
                query = _appDbContext.User.Where(x => x.RoleId == 2 || x.RoleId == 3 || x.RoleId == 4);
            }
            var skipAmount = pageSize * pageIndex;
            var totalRecords = query.Count();
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;

            var result = new PageResult<UserResponse>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;

            int startIndex = (pageIndex * pageSize) + 1;
            var PagedQuery = query.OrderByDescending(x => x.UpdatedOn).Skip(skipAmount).Take(pageSize).ToList();
            result.Results = from x in PagedQuery
                             select (new UserResponse
                             {
                                 RowNum = startIndex++,
                                 UserId = x.UserId,
                                 FirstName = x.FirstName,
                                 LastName = x.LastName,
                                 Email = x.Email,
                                 RoleName = ((Enums.RoleName)x.RoleId).ToString(),
                                 UpdatedOn = x.UpdatedOn
                             });
            return result;
        }

        public PageResult<UserRoleResponse> GetUserByName(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _appDbContext.User.ToList();

            var skipAmount = pageSize * pageIndex;
            var totalRecords = query.Count();
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;

            var result = new PageResult<UserRoleResponse>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;

            int startIndex = (pageIndex * pageSize) + 1;
            var PagedQuery = query.OrderByDescending(x => x.UpdatedOn).Skip(skipAmount).Take(pageSize).ToList();
            result.Results = from x in PagedQuery
                             select (new UserRoleResponse
                             {
                                 RowNum = startIndex++,
                                 UserId = x.UserId,
                                 FirstName = x.FirstName,
                                 Lastname = x.LastName,
                                 FullName = x.FirstName + " " + x.LastName,
                                 Role = new UserRoleIdResponse()
                                 {
                                     id = x.RoleId,
                                     RoleName = ((RoleName)x.RoleId).ToString()
                                 }
                             });
            return result;
        }
        public long UpdateUserRole(IList<UpdateUserRoleRequest> model)
        {
            var updateUserList = new List<User>();

            foreach (var Data in model)
            {
                var query = _appDbContext.User.FirstOrDefault(x => x.UserId == Data.UserId);
                if (!(query is null))
                {
                    query.RoleId = (Int32)Data.RoleName;

                    _emailSenderRepository.SendEmail(query.Email, "botcardadmin@compunnel.net", "Role has been assigned", "$  https://localhost:44315/api/Login/ Your role has been assigned please login with the credentials provided by you at the time of sign up");


                    updateUserList.Add(query);
                }
            }
            foreach (var item in updateUserList)
            {
                _appDbContext.Update(item);
                _appDbContext.SaveChanges();
            }
            return 0;
        }
    }
}
