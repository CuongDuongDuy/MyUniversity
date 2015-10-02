using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyUniversity.Contracts.Metadata
{
    public class StudentProfileMetadata
    {
        [Required]
        [DisplayName("User Name")]
        public virtual string AccountLogin { get; set; }

        [Required]
        [DisplayName("Password")]
        public virtual string AccountPassword { get; set; }

        [DisplayName("Latest Changed")]
        public virtual DateTime? UpdatedOn { get; set; }
    }
}
