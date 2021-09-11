using System;
using IMDBClone.Data.Commons.Enums;

namespace IMDBClone.Data.Entities
{
    public class MovieActor : BaseEntity
    {
        public Guid MovieActorId { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; }
        public ActorRoleEnum Role { get; set; }
    }
}