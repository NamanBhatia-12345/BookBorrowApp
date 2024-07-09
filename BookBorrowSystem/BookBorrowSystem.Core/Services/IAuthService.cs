using BookBorrowSystem.Core.Domain.DTOs;
using BookBorrowSystem.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowSystem.Core.Services
{
    public interface IAuthService
    {
        Task<ApplicationUser> Login(LoginRequestDTO loginRequestDTO);
        Task<bool> RegisterUser(RegisterRequestDTO registerRequestDTO);
        string GenerateTokenString(LoginRequestDTO loginRequestDTO, ApplicationUser user);
    }
}