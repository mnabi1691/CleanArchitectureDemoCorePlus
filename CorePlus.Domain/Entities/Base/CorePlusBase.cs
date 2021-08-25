using System;
using System.Collections.Generic;
using System.Text;

namespace CorePlus.Domain.Entities
{
    public abstract class CorePlusBase
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedByUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
