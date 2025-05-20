using Shopify.Core.Communication;
using Shopify.Core.Entities;
using Shopify.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> isUserEmailExists(string email);
        public Task<User> GetUserByUUID(Guid id);
        public Task<User> GetUserByEmailID(string email);
        public Task<IEnumerable<User>> GetAllCustomers();
        public Task<IEnumerable<User>> GetAllEmployees();
        public Task<bool> ResetNewUserPassword(string email,string password,UserStatusEnum status);
    }
}
