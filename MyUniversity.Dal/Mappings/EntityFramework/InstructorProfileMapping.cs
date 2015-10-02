using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class InstructorProfileMapping : EntityTypeConfiguration<InstructorProfile>
    {
        public InstructorProfileMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.HireDate).IsRequired();

            Property(t => t.EffectiveDate).IsRequired();
            Property(t => t.ExpiryDate);

            Property(t => t.Deactive);
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn);
            Property(t => t.UpdatedBy);

            Map(x => x.MapInheritedProperties());

            HasRequired(e => e.Department)
                .WithMany(e => e.InstructorProfiles)
                .HasForeignKey(t => t.DepartmentId)
                .WillCascadeOnDelete(false);

            ToTable("InstructorProfiles");

        }
    }
}
