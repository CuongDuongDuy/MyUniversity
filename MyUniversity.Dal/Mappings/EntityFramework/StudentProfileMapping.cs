using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class StudentProfileMapping : EntityTypeConfiguration<StudentProfile>
    {
        public StudentProfileMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.EnrollmentDate).IsRequired();

            Property(t => t.EffectiveDate).IsRequired();
            Property(t => t.ExpiryDate);

            Property(t => t.Deactive);
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn);
            Property(t => t.UpdatedBy);

            Map(x => x.MapInheritedProperties());

            HasRequired(e => e.Department)
                .WithMany(e => e.StudentProfiles)
                .HasForeignKey(t => t.DepartmentId)
                .WillCascadeOnDelete(false);

            ToTable("StudentProfiles");
        }
    }
}
