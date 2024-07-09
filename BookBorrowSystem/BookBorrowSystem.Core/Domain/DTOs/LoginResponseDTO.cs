using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBorrowSystem.Core.Domain.DTOs
{
    public class LoginResponseDTO
    {
        public string JwtToken { get; set; }
    }
}
