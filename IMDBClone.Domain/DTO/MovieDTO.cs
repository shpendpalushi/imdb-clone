using System;
using System.Collections.Generic;
using IMDBClone.Data.Commons.Enums;

namespace IMDBClone.Domain.DTO
{
    public class MovieDTO : BaseDTO
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string FileName { get; set; }
        public virtual ICollection<MovieActorDTO> Cast { get; set; }
        public MovieTypeEnum MovieType { get; set; }
    }
}