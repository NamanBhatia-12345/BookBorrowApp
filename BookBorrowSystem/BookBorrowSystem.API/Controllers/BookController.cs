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
    public class BookController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBookRepository bookRepository;
        public BookController(UserManager<ApplicationUser> userManager, IBookRepository bookRepository)
        {
            this.userManager = userManager;
            this.bookRepository = bookRepository;       
        }
        
        //Implement to Create Book
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] AddBookDTO addBookDTO)
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (User.Identity.IsAuthenticated)
                {
                    string userName = User.Identity.Name;
                    string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                    if (userId == null)
                    {
                        return Unauthorized("The User is not exist!!.Please register");
                    }
                }
                var bookDomainModel = new Book
                {
                    Name = addBookDTO.Name,
                    Rating = addBookDTO.Rating,
                    Author = addBookDTO.Author,
                    Genre = addBookDTO.Genre,
                    Description = addBookDTO.Description,
                    LentByUserId = user.Id
                };
                await bookRepository.CreateAsync(bookDomainModel);
                var bookDTO = new BookDTO
                {
                    Id = bookDomainModel.Id,
                    Name = bookDomainModel.Name,
                    Rating = bookDomainModel.Rating,
                    Author = bookDomainModel.Author,
                    Genre = bookDomainModel.Genre,
                    Description = bookDomainModel.Description,
                    LentByUserId = bookDomainModel.LentByUserId
                };
                return CreatedAtAction(nameof(GetBookById), new { Id = bookDTO.Id }, bookDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }


        // Implement to Get the Details of Book by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            try
            {
                var bookDomainModel = await bookRepository.GetByIdAsync(id);
                if (bookDomainModel == null)
                {
                    return NotFound($"The Book does not exist with id :- {id}");
                }
                var bookDTO = new BookDTO
                {
                    Id = bookDomainModel.Id,
                    Name = bookDomainModel.Name,
                    Rating = bookDomainModel.Rating,
                    Author = bookDomainModel.Author,
                    Genre = bookDomainModel.Genre,
                    Description = bookDomainModel.Description,
                    IsBookAvailable = bookDomainModel.IsBookAvailable,
                    LentByUserId = bookDomainModel.LentByUserId,
                    BorrowedByUserId = bookDomainModel.BorrowedByUserId
                };
                return Ok(bookDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        //Implement to Get all the books

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string filterOn, [FromQuery] string filterQuery)
        {
            try
            {
                var books = await bookRepository.GetAllAsync(filterOn, filterQuery);
                var bookDTO = new List<BookDTO>();
                foreach (var book in books)
                {
                    bookDTO.Add(new BookDTO
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Rating = book.Rating,
                        Author = book.Author,
                        Genre = book.Genre,
                        Description = book.Description,
                        IsBookAvailable = book.IsBookAvailable,
                        LentByUserId = book.LentByUserId,
                        BorrowedByUserId = book.BorrowedByUserId
                    });
                }
                return Ok(bookDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        //Implement to Update the Book. Update book only possible if BookIsAvailable

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDTO updateBookDTO)
        {
            try
            {
                var bookModel = await bookRepository.GetByIdAsync(id);
                if (bookModel != null && bookModel.IsBookAvailable == false)
                {
                    return NotFound($"Book with Name :- {bookModel.Name} and its author :- {bookModel.Author} cannot be updated because it is already borrowed by the user.");
                }
                var bookDomainModel = new Book
                {
                    Name = updateBookDTO.Name,
                    Rating = updateBookDTO.Rating,
                    Author = updateBookDTO.Author,
                    Genre = updateBookDTO.Genre,
                    Description = updateBookDTO.Description,
                    IsBookAvailable = updateBookDTO.IsBookAvailable
                };
                bookDomainModel = await bookRepository.UpdateAsync(id, bookDomainModel);
                if (bookDomainModel == null)
                {
                    return NotFound("The Book does not exist");
                }
                var bookDTO = new BookDTO
                {
                    Id = bookDomainModel.Id,
                    Name = bookDomainModel.Name,
                    Rating = bookDomainModel.Rating,
                    Author = bookDomainModel.Author,
                    Genre = bookDomainModel.Genre,
                    Description = bookDomainModel.Description,
                    LentByUserId = bookDomainModel.LentByUserId,
                    BorrowedByUserId = bookDomainModel.BorrowedByUserId
                };
                return Ok(bookDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        //Implement to Delete the Book.

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            try
            {
                var bookModel = await bookRepository.GetByIdAsync(id);
                if (bookModel != null && bookModel.IsBookAvailable == false)
                {
                    return NotFound($"Book with Name :- {bookModel.Name} and its author :- {bookModel.Author} cannot be deleted because it is already borrowed by the user.");
                }
                var bookDomainModel = await bookRepository.DeleteAsync(id);
                if (bookDomainModel == null)
                {
                    return NotFound("The Book does not exist");
                }
                var bookDTO = new BookDTO
                {
                    Id = bookDomainModel.Id,
                    Name = bookDomainModel.Name,
                    Rating = bookDomainModel.Rating,
                    Author = bookDomainModel.Author,
                    Genre = bookDomainModel.Genre,
                    Description = bookDomainModel.Description,
                    LentByUserId = bookDomainModel.LentByUserId,
                    BorrowedByUserId = bookDomainModel.BorrowedByUserId
                };
                return Ok(bookDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing a book: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
