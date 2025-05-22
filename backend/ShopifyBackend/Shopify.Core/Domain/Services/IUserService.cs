using Shopify.Core.Communication;
using Shopify.Core.Entities;
using Shopify.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> ListAllUsersAsync();
        public Task<bool> isUserExists(string mail);
        public Task<User> GetUserDetailsByUUID(Guid id);
        public Task<bool> CreateUser(User newUser);
        public Task<bool> UpdateUser(User newUserData);
        public Task<User> GetUserByEmail(string email);
        public Task<ServiceResult> VerifyUserLogin(string email, string password);
        public Task<ServiceResult> ResetNewUserPassword(string email,string password,UserStatusEnum status);
        public Task<List<string>> SearchUserByEmail(string email);
    }
}
