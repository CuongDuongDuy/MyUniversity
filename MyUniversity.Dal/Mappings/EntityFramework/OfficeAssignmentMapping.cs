using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class OfficeAssignmentMapping : EntityTypeConfiguration<OfficeAssignment>
    {
        public OfficeAssignmentMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Location).IsRequired().HasMaxLength(250);
            Property(t => t.WorkingHours).IsRequired().HasMaxLength(100);
            Property(t => t.Phone).IsRequired().HasMaxLength(50);
            Property(t => t.DepartmentId).IsOptional();

            Property(t => t.Deactive);
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn);
            Property(t => t.UpdatedBy);

            HasOptional(t => t.Department).WithMany(c => c.OfficeAssignments).HasForeignKey(t => t.DepartmentId).WillCascadeOnDelete(false);

            ToTable("OfficeAssignments");
        }
    }
}
