using System;
using System.Runtime.Serialization;

namespace MyUniversity.Contracts.Models
{
    [DataContract(IsReference = true)]
    public class BaseModel
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedOn { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
        [DataMember]
        public DateTime? UpdatedOn { get; set; }
        [DataMember]
        public bool? Deactive { get; set; }
    }
}
