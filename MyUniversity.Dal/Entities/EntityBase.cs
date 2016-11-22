using System;
using System.ComponentModel.DataAnnotations;

namespace MyUniversity.Dal.Entities
{
    public abstract class EntityBase
    {
        public virtual Guid Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedOn { get; set; }
        public virtual bool? Deactive { get; set; }
        [Timestamp]
        [ConcurrencyCheck]
        public virtual byte[] RowVersion { get; set; }
    }
}