using BookBorrowSystem.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Core.IRepositories
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetUserDetailsByIdAsync(string userId);
        Task<bool> Update(string userId, ApplicationUser updatedUser);  
    }
}
