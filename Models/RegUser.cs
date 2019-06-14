using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginReg.Models
{
    public class RegUser
    {
        [Key]
        public int UserId {get;set;}
        [Required(ErrorMessage = " First name is required")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long")]
        public string Fname { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters long")]
        public string Lname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email field is required")]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string cPassword {get;set;}
    }
}