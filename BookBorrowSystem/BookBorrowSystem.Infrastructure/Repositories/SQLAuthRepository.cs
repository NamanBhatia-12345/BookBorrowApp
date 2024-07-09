using BookBorrowSystem.Core.Domain.Models;
using BookBorrowSystem.Core.IRepositories;
using BookBorrowSystem.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Infrastructure.Repositories
{
    public class SQLAuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SQLAuthRepository(UserManager<ApplicationUser> _userManager)
        {
            this._userManager = _userManager;
        }
        public async Task<ApplicationUser> GetUserDetailsByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred during model building: {ex.Message}");
                throw; 
            }
        }
        public async Task<bool> Update(string userId, ApplicationUser updatedUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }
                user.TokensAvailable = updatedUser.TokensAvailable;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred during model building: {ex.Message}");
                throw; 
            }
        }
    }
}
