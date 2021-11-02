using PlanningPoker.WebApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.WebApi.Services.Interfaces
{
    public interface ITokenService
    {
        long AddForUser(long id, string email, string jwttoken);
        string GetToken(string jwttoken);

    }
}
