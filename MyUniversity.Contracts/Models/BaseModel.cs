using System;

namespace MyUniversity.Contracts.Models
{
    public class BaseModel
    {
        public virtual Guid Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedOn { get; set; }
    }
}
