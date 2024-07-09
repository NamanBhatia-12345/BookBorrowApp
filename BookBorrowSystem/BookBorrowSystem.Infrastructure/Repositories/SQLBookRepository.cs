using BookBorrowSystem.Core.Domain.Models;
using BookBorrowSystem.Core.IRepositories;
using BookBorrowSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BookBorrowSystem.Infrastructure.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly BookDbContext dbContext;

        public SQLBookRepository(BookDbContext dbContext)
        {
             this.dbContext = dbContext;
        }
        public async Task<Book> CreateAsync(Book book)
        {
            try
            {
                await dbContext.Books.AddAsync(book);
                await dbContext.SaveChangesAsync();
                return book;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<Book> DeleteAsync(int id)
        {
            try
            {
                var book = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return null;
                }
                dbContext.Books.Remove(book);
                await dbContext.SaveChangesAsync();
                return book;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Book>> GetAllAsync(string filterOn = null, string filterQuery = null)
        {
            try
            {
                var books = dbContext.Books.AsQueryable();
                if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
                {
                    books = books.Where(x => x.IsBookAvailable == true);
                    if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        books = books.Where(x => x.Name.Contains(filterQuery));
                    }
                    if (filterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
                    {
                        books = books.Where(x => x.Author.Contains(filterQuery));
                    }
                    if (filterOn.Equals("Genre", StringComparison.OrdinalIgnoreCase))
                    {
                        books = books.Where(x => x.Genre.Contains(filterQuery));
                    }
                }
                return await books.ToListAsync();
            }
            catch( Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            try
            {
                var book = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
                return book;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            try
            {
                var bookDomainModel = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (bookDomainModel == null)
                {
                    return null;
                }
                bookDomainModel.Name = book.Name;
                bookDomainModel.Description = book.Description;
                bookDomainModel.Author = book.Author;
                bookDomainModel.Rating = book.Rating;
                bookDomainModel.Genre = book.Genre;
                bookDomainModel.IsBookAvailable = book.IsBookAvailable;
                await dbContext.SaveChangesAsync();
                return bookDomainModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<Book> UpdateAvailableStatusBookAsync(int id, Book book, bool flag)
        {
            try
            {
                var bookModel = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (bookModel == null)
                {
                    return null;
                }
                bookModel.IsBookAvailable = flag;
                await dbContext.SaveChangesAsync();
                return bookModel;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<List<Book>> GetAllBorrowBookAsync(string userId)
        {
            try
            {
             var borrowedBooks = await dbContext.Books
                    .Where(b => b.BorrowedByUserId == userId)
                    .ToListAsync();
                return borrowedBooks;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;

            }
            
        }
        public async Task<List<Book>> GetAllBooksCreatedAsync(string userId)
        {
            try
            {
                var createdBooks = await dbContext.Books
                       .Where(b => b.LentByUserId == userId)
                       .ToListAsync();
                return createdBooks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
