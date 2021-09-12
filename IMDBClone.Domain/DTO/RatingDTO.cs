using System;
using IMDBClone.Domain.DTO.User;

namespace IMDBClone.Domain.DTO
{
    public class RatingDTO : BaseDTO
    {
        public Guid RatingId { get; set; }
        public int Rate { get; set; }
        public ApplicationUserDTO User { get; set; }
        public Guid UserId { get; set; }
        public MovieDTO Movie { get; set; }
        public Guid MovieId { get; set; }
    }
}