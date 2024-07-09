using BookBorrowSystem.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Core.Domain.DTOs
{
    public class BorrowDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public bool IsBookAvailable { get; set; } = true;

        [ForeignKey("LentByUserId")]
        public string LentByUserId { get; set; }
        public string FullName { get; set; }
        public int TokensAvailable { get; set; } = 5;
        public string BorrowedByUserId { get; set; } = null;
    }
}
