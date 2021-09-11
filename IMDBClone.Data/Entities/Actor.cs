using System;
using System.Collections.Generic;

namespace IMDBClone.Data.Entities
{
    public class Actor : BaseEntity
    {
        public Guid ActorId { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<MovieActor> Movies { get; set; }
    }
}