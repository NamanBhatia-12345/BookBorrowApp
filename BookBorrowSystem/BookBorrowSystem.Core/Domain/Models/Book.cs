using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Core.Domain.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Author { get; set; }
        public string Genre  { get; set; }
        public string Description { get; set; }
        public bool IsBookAvailable { get; set; } = true;

        [ForeignKey("LentByUserId")]
        public string LentByUserId { get; set; }
        //Navigation Property 
        public ApplicationUser LentByUser { get; set; }
        public string BorrowedByUserId { get; set; } = null;
    }
}