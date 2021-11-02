using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.Repository.Interface;
using PlanningPoker.Domain.Entities;
using System.Linq;
using PlanningPoker.Common;
using PlanningPoker.DataContract.Response;

namespace PlanningPoker.Repository.Class
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _appDbContext;
        public UserRepository(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User GetByEmail(string email)
        {
            var query = _appDbContext.User;
            var final = from q in query.Where(c => c.Email == email)
                        select q;
            if (final.Count() > 0)
                return final.FirstOrDefault();
            else
                return null;
        }
        public bool UpdatePasswordByEmail(long id, string newpassword, string jwttoken)
        {
            var query = _appDbContext.User.ToList();
            var uquery = _appDbContext.ResetPassword.ToList();
            var tokenQuery = _appDbContext.ResetPassword.Select(x => x.UserId).ToList();
            var equalQuery = query.Where(x => tokenQuery.Any(y => y == x.UserId)).ToList();
            var mailQuery = query.FirstOrDefault(x => x.UserId == id);
            if (!(mailQuery is null))
            {

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(newpassword, out passwordHash, out passwordSalt);

                mailQuery.PasswordHash = passwordHash;
                mailQuery.PasswordSalt = passwordSalt;

                var updatedpassword = this._appDbContext.Update(mailQuery);
                _appDbContext.SaveChanges();


                if (!(equalQuery is null))
                {
                    var getToken = _appDbContext.ResetPassword.Where(y => y.ResetToken == jwttoken).FirstOrDefault();
                    getToken.ResetToken = null;
                    _appDbContext.Update(getToken);
                    _appDbContext.SaveChanges();
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _appDbContext.User.SingleOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public PageResult<RegUserResponse> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _appDbContext.User.ToList();
            var skipAmount = pageSize * pageIndex;
            var totalRecords = query.Count;
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;

            var result = new PageResult<RegUserResponse>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;
            var PagedQuery = query.OrderBy(x => x.UserId).Skip(skipAmount).Take(pageSize).ToList();
            result.Results = from x in PagedQuery
                             select (new RegUserResponse
                             {
                                 UserId = x.UserId,
                                 FirstName = x.FirstName,
                                 LastName = x.LastName,
                                 Email = x.Email,
                                 CreatedOn = x.CreatedOn
                             });
            return result;
        }

        public User GetById(long id)
        {
            return _appDbContext.User.Find(id);
        }

        public User Create(User user, string password, string firstname, string lastname)
        {
            if (!string.IsNullOrWhiteSpace(firstname))
                user.FirstName = firstname;
            user.LastName = lastname;


            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_appDbContext.User.Any(x => x.Email == user.Email))
                throw new AppException("Username \"" + user.Email + "\" is already taken");
            user.RoleId = (Int32)Enums.RoleName.NotAssigned;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.CreatedOn = DateTime.Now;
            user.UpdatedOn = DateTime.Now;
            user.CreatedBy = 0;
            user.UpdatedBy = 0;
            user.RowGuid = Guid.NewGuid();

            _appDbContext.User.Add(user);
            _appDbContext.SaveChanges();

            return user;
        }

      

        public void Delete(long id)
        {
            var user = _appDbContext.User.Find(id);
            if (!(user is null))
            {
                _appDbContext.User.Remove(user);
                _appDbContext.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }
}
