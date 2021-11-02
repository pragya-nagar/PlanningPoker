using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Interface
{
    public interface IResetPasswordService
    {
        long AddForUser(long id, string email, string jwttoken);
        string GetToken(string jwttoken);
    }
}
