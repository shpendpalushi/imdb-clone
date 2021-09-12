using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IMDBClone.Domain.DTO
{
    public class ActorDTO : BaseDTO
    {
        public Guid ActorId { get; set; }
        public string FullName { get; set; }
        [JsonIgnore]
        public virtual ICollection<MovieActorDTO> Movies { get; set; }
        
    }
}