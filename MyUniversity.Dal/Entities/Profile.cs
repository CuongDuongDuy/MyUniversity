using System;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public abstract class Profile : EntityBase
    {
        public virtual DateTime EffectiveDate { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual Guid PersonId { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }

    }
}
