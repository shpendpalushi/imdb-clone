using System;
using System.Collections.Generic;

namespace IMDBClone.Domain.DTO
{
    public class ActorDTO : BaseDTO
    {
        public Guid ActorId { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<MovieActorDTO> Movies { get; set; }
        
    }
}