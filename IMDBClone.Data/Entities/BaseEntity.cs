using System;

namespace IMDBClone.Data.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreateIP { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedIP { get; set; }
        public bool IsDeleted { get; set; }
    }
}