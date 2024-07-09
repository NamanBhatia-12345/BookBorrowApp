using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Core.Domain.DTOs
{
    public class BookDTO
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
        public string BorrowedByUserId { get; set; } = null;
    }
}
