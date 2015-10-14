using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class EnrollmentMapping : EntityTypeConfiguration<Enrollment>
    {
        public EnrollmentMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CourseId).IsRequired();
            Property(t => t.StudentProfileId).IsRequired();
            Property(t => t.InstructorProfileId).IsRequired();
            Property(t => t.Mark).IsOptional();
            Property(t => t.Deactive);
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn);
            Property(t => t.UpdatedBy);

            HasRequired(t => t.Course)
               .WithMany(t => t.Enrollments)
               .HasForeignKey(t => t.CourseId)
               .WillCascadeOnDelete(false);

            HasRequired(t => t.StudentProfile)
                .WithMany(t => t.Enrollments)
                .HasForeignKey(t => t.StudentProfileId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.InstructorProfile)
               .WithMany(t => t.Enrollments)
               .HasForeignKey(t => t.InstructorProfileId)
               .WillCascadeOnDelete(false);

            ToTable("Enrollments");
        }
    }
}
