using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using IMDBClone.Data.Commons.Enums;

namespace IMDBClone.Data.Entities
{
    public class Movie : BaseEntity
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string FileName { get; set; }
        public virtual ICollection<MovieActor> Cast { get; set; }
        public MovieTypeEnum MovieType { get; set; }
        public virtual ICollection<Rating> Ratings{ get; set; }
        
    }
}