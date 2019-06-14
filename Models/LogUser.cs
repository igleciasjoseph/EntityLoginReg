using System;
using System.ComponentModel.DataAnnotations;
namespace LoginReg.Models
{
    public class LogUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Please enter a password before logging in!")]
        public string Password { get; set; }

    }
}