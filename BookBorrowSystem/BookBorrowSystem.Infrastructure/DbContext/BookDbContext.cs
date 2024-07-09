using BookBorrowSystem.Core.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Infrastructure.DbContext
{
    public class BookDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
              
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Book>()
                    .HasOne(b => b.LentByUser)
                    .WithMany()
                    .HasForeignKey(b => b.LentByUserId);

                //modelBuilder.Entity<BorrowBook>()
                //    .HasOne(bb => bb.Book)
                //    .WithMany()
                //    .HasForeignKey(bb => bb.BookId);

                //modelBuilder.Entity<BorrowBook>()
                //    .HasOne(bb => bb.User)
                //    .WithMany()
                //    .HasForeignKey(bb => bb.UserId);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"An exception occurred during model building: {ex.Message}");
                throw;
            }
        }

        public DbSet<Book> Books { get; set;}
        //public DbSet<BorrowBook> BorrowBooks { get; set; } 
    }
}