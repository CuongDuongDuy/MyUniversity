using System;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public abstract class Profile : EntityBase
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Guid PersonId { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }

    }
}
