using System;

namespace MyUniversity.Contracts.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Deactive { get; set; }
    }
}
