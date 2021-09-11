using System;
using IMDBClone.Data.Commons.Enums;
using IMDBClone.Data.Entities;

namespace IMDBClone.Domain.DTO
{
    public class MovieActorDTO : BaseDTO
    {
        public Guid MovieActorId { get; set; }
        public Guid MovieId { get; set; }
        public MovieDTO Movie { get; set; }
        public Guid ActorId { get; set; }
        public ActorDTO Actor { get; set; }
        public ActorRoleEnum Role { get; set; }
    }
}