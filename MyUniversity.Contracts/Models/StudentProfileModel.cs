using System.ComponentModel.DataAnnotations;
using MyUniversity.Contracts.Metadata;

namespace MyUniversity.Contracts.Models
{
    [MetadataType(typeof (StudentProfileMetadata))]
    public class StudentProfileModel : BaseModel
    {
        public virtual bool? Deactive { get; set; }

        public virtual string AccountLogin { get; set; }

        public virtual string AccountPassword { get; set; }

    }
}
