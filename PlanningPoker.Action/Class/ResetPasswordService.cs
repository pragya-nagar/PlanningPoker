using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.Repository.Interface;
using PlanningPoker.Action.Interface;

namespace PlanningPoker.Action.Class
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IResetPasswordRepository _resetPasswordRepository;

        public ResetPasswordService(IResetPasswordRepository resetPasswordRepository)
        {
            _resetPasswordRepository = resetPasswordRepository;
        }

        public long AddForUser(long id, string email, string jwttoken)
        {
            var result = _resetPasswordRepository.AddForUser(id, email, jwttoken);
            return result;
        }

        public string GetToken(string jwttoken)
        {
            var result = _resetPasswordRepository.GetToken(jwttoken);
            return result;
        }
    }
}
