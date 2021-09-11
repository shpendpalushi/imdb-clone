using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Domain.DTO.User
{
    public class RegisterDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}