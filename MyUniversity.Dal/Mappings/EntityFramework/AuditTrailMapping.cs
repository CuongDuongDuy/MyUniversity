using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class AuditTrailMapping : EntityTypeConfiguration<AuditTrail>
    {
        public AuditTrailMapping()
        {
            HasKey(t => t.Guid);

            Property(t => t.Guid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ActionType).IsRequired().HasMaxLength(50);
            Property(t => t.Value).IsRequired();

            ToTable("AuditTrails");
        }
    }
}
