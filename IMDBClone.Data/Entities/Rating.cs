using System;

namespace IMDBClone.Data.Entities
{
    public class Rating : BaseEntity
    {
        public Guid RatingId { get; set; }
        public int Rate { get; set; }
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
    }
}