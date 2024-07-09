using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Core.Domain.DTOs
{
    public class UpdateBookDTO
    {
        [Required]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^(10|[0-9])$", ErrorMessage = "Rating must be a number from 0 to 10")]
        public int Rating { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only alphabets and spaces are allowed")]
        [StringLength(30, ErrorMessage = "Author cannot exceed 30 characters")]
        public string Author { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only alphabets and spaces are allowed")]
        [StringLength(30, ErrorMessage = "Genre cannot exceed 30 characters")]
        public string Genre { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsBookAvailable { get; set; } = true;

    }
}
