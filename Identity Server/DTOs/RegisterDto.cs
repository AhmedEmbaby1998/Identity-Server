using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Server.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Name { set; get; }
        [Required]
        [Phone]
        public string Phone { set; get; }
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        public string Password { set; get; }
    }
}
