using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Domain.DTO.User
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public override string ToString()
        {
            return $"Username: {Username};Password: {Password}";
        }
    }
}