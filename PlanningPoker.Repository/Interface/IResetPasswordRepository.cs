using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.Domain.Entities;

namespace PlanningPoker.Repository.Interface
{
    public interface IResetPasswordRepository
    {
        long AddForUser(long id, string email, string jwttoken);
        string GetToken(string jwttoken);
    }
}
