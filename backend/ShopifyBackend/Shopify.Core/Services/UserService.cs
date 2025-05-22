using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Shopify.Core.Communication;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.Domain.Services;
using Shopify.Core.Entities;
using Shopify.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> ListAllUsersAsync()
        {
            return await _unitOfWork.User.GetAllAsync();
        }

        public async Task<bool> isUserExists(string mail)
        {
            return await _unitOfWork.User.isUserEmailExists(mail);
        }

        public async Task<User> GetUserDetailsByUUID(Guid UUID)
        {
            return await _unitOfWork.User.GetUserByUUID(UUID);
        }

        public async Task<bool> CreateUser(User newUser)
        {
            try
            {
                await _unitOfWork.User.AddAsync(newUser);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(User newUserData)
        {
            try
            {
                _unitOfWork.User.Update(newUserData);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _unitOfWork.User.GetUserByEmailID(email);
        }

        public async Task<ServiceResult> VerifyUserLogin(string email, string password)
        {
            var userInDB = await GetUserByEmail(email);
            if (userInDB == null)
            {
                return new ServiceResult(false, "Email doesn't exist");
            }
            bool isVerifiedPassword = BCrypt.Net.BCrypt.Verify(password, userInDB.PasswordHash);
            var lastlogin = userInDB.LastLogin;
            if (isVerifiedPassword)
            {
                if (lastlogin == null)
                {
                    lastlogin = DateTime.Now;
                }
                userInDB.LastLogin = DateTime.Now;
                _unitOfWork.User.Update(userInDB);
                await _unitOfWork.SaveAsync();
                return new ServiceResult(true, "Verification Successful");
            }
            return new ServiceResult(false, "Incorrect Password");
        }

        public async Task<ServiceResult> ResetNewUserPassword(string email,string password, UserStatusEnum status)
        {
            try
            {
                var reset = await _unitOfWork.User.ResetNewUserPassword(email, password,status);
                if (reset)
                {
                    return new ServiceResult(true, "Password reseted, Please Login to continue");
                }
                else
                {
                    return new ServiceResult(false, "Failed to reset password");
                }

            }
            catch (Exception ex)
            {
                return new ServiceResult(false, "Failed to reset password");
            }
        }

        public async Task<List<string>> SearchUserByEmail(string email)
        {
            return await _unitOfWork.User.SearchUserByEmail(email);
        }
    }
}
