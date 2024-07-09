using BookBorrowSystem.Core.Domain.DTOs;
using BookBorrowSystem.Core.Domain.Models;
using BookBorrowSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BookBorrowSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly UserManager<ApplicationUser> userManager;
        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            this.authService = authService;
            this.userManager = userManager;
        }

        //Implement to Register the User
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                var result = await authService.RegisterUser(registerRequestDTO);
                if (result)
                {
                    return Ok("Registered Successfully!!!");
                }
                return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }


        //Implement the Login Functionality
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Username or password are not properly defined");
                }
                var user = await authService.Login(loginRequestDTO);
                if (user != null)
                {
                    var tokenString = authService.GenerateTokenString(loginRequestDTO, user);
                    var response = new LoginResponseDTO
                    {
                        JwtToken = tokenString
                    };
                    return Ok(new
                    {
                        Message = "Login Successfully!!",
                        Token = response
                    });
                }
                return BadRequest("Username or password incorrect");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }


        //Implement to Get The Details of Logged In User
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetDetailsOfLoggedInUser()
        {
            try
            {
                if (User.Identity.IsAuthenticated == false)
                {
                    return Unauthorized("Please login first!!");
                }
                var user = await userManager.GetUserAsync(HttpContext.User);
                var getLoggedInUserDTO = new GetLoggedInUserDTO
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Name = user.Name,
                    TokensAvailable = user.TokensAvailable
                };
                return Ok(getLoggedInUserDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
