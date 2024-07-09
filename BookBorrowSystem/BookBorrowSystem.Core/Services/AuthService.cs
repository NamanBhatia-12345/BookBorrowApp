using BookBorrowSystem.Core.Domain.DTOs;
using BookBorrowSystem.Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookBorrowSystem.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration _config)
        {
             this.userManager = userManager;
             this._config = _config;
        }
        public async Task<bool> RegisterUser([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                var applicationUser = new ApplicationUser
                {
                    UserName = registerRequestDTO.Username,
                    Email = registerRequestDTO.Username,
                    Name = registerRequestDTO.Name
                };
                var result = await userManager.CreateAsync(applicationUser, registerRequestDTO.Password);
                return result.Succeeded;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        [HttpPost]
        public async Task<ApplicationUser> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var applicationUser = await userManager.FindByEmailAsync(loginRequestDTO.Username);
                var result = await userManager.CheckPasswordAsync(applicationUser, loginRequestDTO.Password);
                if (applicationUser != null && result)
                {
                    return applicationUser;
                }
                return null;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public string GenerateTokenString(LoginRequestDTO loginRequestDTO, ApplicationUser user)
        {
            try
            {
               var claims = new List<Claim>
               {
                    new Claim(ClaimTypes.Email,loginRequestDTO.Username),
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Name", user.Name)
               };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
                var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    issuer: _config.GetSection("Jwt:Issuer").Value,
                    audience: _config.GetSection("Jwt:Audience").Value,
                    signingCredentials: signingCred);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return tokenString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
