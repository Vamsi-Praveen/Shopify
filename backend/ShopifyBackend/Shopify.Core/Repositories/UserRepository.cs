﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopify.Core.Data;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using Shopify.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Repositories
{
    public class UserRepository : GenericRepository<User, ShopifyContext>,
            IUserRepository
    {
        private readonly ILogger _logger;
        public UserRepository(ShopifyContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> isUserEmailExists(string email)
        {
            try
            {
                return await Context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
            }
            catch (Exception error)
            {
                _logger.LogError("IsScopeExistsWithNameAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<User> GetUserByUUID(Guid id)
        {
            try
            {
                return await Context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception error)
            {
                _logger.LogError("GetUserByUUID::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<User> GetUserByEmailID(string email)
        {
            try
            {
                return await Context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception error)
            {
                _logger.LogError("IsScopeExistsWithNameAsync::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAllCustomers()
        {
            try
            {
                return await Context.Users
                        .AsNoTracking()
                        .Where(u => u.Role == UserRole.Customer.ToString())
                        .ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllCustomers::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAllEmployees()
        {
            try
            {
                return await Context.Users
                        .AsNoTracking()
                        .Where(u => u.Role == UserRole.Employee.ToString())
                        .ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllEmployees::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<bool> ResetNewUserPassword(string email, string password, UserStatusEnum status)
        {
            try
            {
                var userInDb = await GetUserByEmailID(email);
                userInDb.PasswordHash = password;
                userInDb.Status = status.ToString();

                Context.Users.Update(userInDb);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {
                _logger.LogError("ResetNewUserPassword::Database exception: {0}", err);
                return false;
            }
        }

        public async Task<List<string>> SearchUserByEmail(string email)
        {
            try
            {
                return await Context.Users
                .Where(p => p.Email.ToLower().Contains(email))
                .Select(p => p.Email)
                .ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("SearchUserByEmail::Database exception: {0}", error);
                return null;
            }
        }
    }
}
