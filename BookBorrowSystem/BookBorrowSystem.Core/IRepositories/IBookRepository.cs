using BookBorrowSystem.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Core.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(string filterOn = null, string filterQuery = null);
        Task<Book> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(int id, Book book);
        Task<Book> DeleteAsync(int id);
        Task<Book> UpdateAvailableStatusBookAsync(int id, Book book, bool flag);
        Task<List<Book>> GetAllBorrowBookAsync(string userId);
        Task<List<Book>> GetAllBooksCreatedAsync(string userId);
    }
}