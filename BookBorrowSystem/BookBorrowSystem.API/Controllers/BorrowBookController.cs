using BookBorrowSystem.Core.Domain.DTOs;
using BookBorrowSystem.Core.Domain.Models;
using BookBorrowSystem.Core.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookBorrowSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBookRepository bookRepository;
        private readonly IAuthRepository authRepository;

        public BorrowBookController(UserManager<ApplicationUser> userManager, IBookRepository bookRepository, IAuthRepository authRepository)
        {
            this.userManager = userManager;
            this.bookRepository = bookRepository;
            this.authRepository = authRepository;
        }

        //Implement to Borrow Book. Borrow Book is only possible when the User Login First.

        [HttpGet("{bookId:int}")]
        [Authorize]
        public async Task<IActionResult> BorrowBook([FromRoute] int bookId)
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (User.Identity.IsAuthenticated == false)
                {
                    return Unauthorized("Please Lgin First!!");
                }
                var book = await bookRepository.GetByIdAsync(bookId);
                if (book == null)
                {
                    return NotFound("Book not found.");
                }
                if(user.TokensAvailable == 0)
                {
                    return BadRequest("The Tokens is not available to borrow book."); 
                }
                if (user.Id == book.LentByUserId)
                {
                    return BadRequest($"Book not borrow to the user. The {user.Name} which lent the book try to borrow the same book.");
                }
                if (book.IsBookAvailable == false)
                {
                    return BadRequest("Book is not available to borrow.");
                }
                book.BorrowedByUserId = user.Id;
                await bookRepository.UpdateAvailableStatusBookAsync(book.Id, book, false);
                user.TokensAvailable--;
                await authRepository.Update(user.Id, user);
                var userModel = await authRepository.GetUserDetailsByIdAsync(book.LentByUserId);
                if (userModel == null)
                {
                    return NotFound($"The user does not exists :- {userModel.Id}");
                }
                userModel.TokensAvailable++;
                await authRepository.Update(userModel.Id, userModel);
                var borrowDTO = new BorrowDTO
                {
                    Id = book.Id,
                    Name = book.Name,
                    Rating = book.Rating,
                    Author = book.Author,
                    Genre = book.Genre,
                    Description = book.Description,
                    IsBookAvailable = book.IsBookAvailable,
                    LentByUserId = book.LentByUserId,
                    BorrowedByUserId = user.Id,
                    FullName = user.Name,
                    TokensAvailable = user.TokensAvailable
                };
                return Ok(borrowDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }


        // Implement to get the details of all Get All Borrow Books By User
        [HttpGet("GetAllBorrowBooksByUser")]
        public async Task<IActionResult> GetAllBorrowBookByUser()
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (User.Identity.IsAuthenticated == false || user.Id == null)
                {
                    return Unauthorized("The User is not exist!!.Please register");
                }
                var borrowBooks = await bookRepository.GetAllBorrowBookAsync(user.Id);
                return Ok(borrowBooks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }


        // Implement to get the details of all Get All Books Created By User
        [HttpGet("GetAllBooksCreatedByUser")]
        public async Task<IActionResult> GetAllBooksCreatedByUser()
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (User.Identity.IsAuthenticated == false || user.Id == null)
                {
                    return Unauthorized("The User is not exist!!.Please register");
                }
                var createdBooks = await bookRepository.GetAllBooksCreatedAsync(user.Id);
                return Ok(createdBooks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }


        // Implement to return Book.
        [HttpGet("ReturnBook/{bookId:int}")]
        [Authorize]
        public async Task<IActionResult> ReturnBook([FromRoute] int bookId)
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (User.Identity.IsAuthenticated == false)
                {
                    return Unauthorized("Please Login First!!");
                }
                var book = await bookRepository.GetByIdAsync(bookId);
                if (book == null)
                {
                    return NotFound("Book not found.");
                }
                if (book.IsBookAvailable == true)
                {
                    return BadRequest($"The book with Book Id:- {book.Id} is already returned.");
                }
                book.BorrowedByUserId = null;
                await bookRepository.UpdateAvailableStatusBookAsync(book.Id, book, true);
                return Ok($"The book with Book Id: -{book.Id} and Name :- {book.Name} is returned successfully by the user :- {user.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
