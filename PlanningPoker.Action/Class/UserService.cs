using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.Action.Interface;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;

namespace PlanningPoker.Action.Class
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string email, string password)
        {
            var result = _userRepository.Authenticate(email, password);
            return result;
        }
        public PageResult<RegUserResponse> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _userRepository.GetAll(pageIndex, pageSize);
            return result;
        }
        public User GetById(long userId)
        {
            var result = _userRepository.GetById(userId);
            return result;
        }
        public User Create(User user, string password, string firstname, string lastname)
        {
            var result = _userRepository.Create(user, password, firstname, lastname);
            return result;
        }
        
        public void Delete(long id)
        {
            _userRepository.Delete(id);
            //return result;
        }
        public User GetByEmail(string email)
        {
            var result = _userRepository.GetByEmail(email);
            return result;
        }
        public bool UpdatePasswordByEmail(long id, string newpassword, string jwttoken)
        {
            var result = _userRepository.UpdatePasswordByEmail(id, newpassword, jwttoken);
            return result;
        }
    }
}
