using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Interface
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        PageResult<RegUserResponse> GetAll(int pageIndex = 0, int pageSize = int.MaxValue);
        User GetById(long id);
        User Create(User user, string password, string firstname, string lastname);
        void Delete(long id);
        User GetByEmail(string email);
        bool UpdatePasswordByEmail(long id, string newpassword, string jwttoken);
    }
}
