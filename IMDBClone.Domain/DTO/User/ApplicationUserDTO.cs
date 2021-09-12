using System;

namespace IMDBClone.Domain.DTO.User
{
    public class ApplicationUserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}