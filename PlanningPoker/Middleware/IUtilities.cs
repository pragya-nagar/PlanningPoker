using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.WebApi.Middleware
{
   public interface IUtilities
    {
        string GenerateResetToken(long id,string email);
        string ResetMailAsync(long id, string email);
        string GetSubFromToken(string token);
    }
}
