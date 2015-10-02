using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class CourseMapping : EntityTypeConfiguration<Course>
    {
        public CourseMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Title).IsRequired().HasMaxLength(250);
            Property(t => t.Credits).IsRequired();
            Property(t => t.DepartmentId).IsRequired();

            HasRequired(e => e.Department)
                .WithMany(e => e.Courses)
                .HasForeignKey(t => t.DepartmentId)
                .WillCascadeOnDelete(false);

            HasMany(e => e.InstructorProfiles)
                .WithMany(e => e.Courses)
                .Map(t => t.MapLeftKey("CouseId").MapRightKey("InstructorId").ToTable("CourseInstructor"));

            ToTable("Courses");
        }
    }
}
