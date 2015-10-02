using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class PersonMapping : EntityTypeConfiguration<Person>
    {
        public PersonMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IdentityNumber).IsRequired().HasMaxLength(20);
            Property(t => t.FirstName).IsRequired().HasMaxLength(50);
            Property(t => t.LastName).IsRequired().HasMaxLength(50);
            Property(t => t.DateOfBirth).IsRequired();
            Property(t => t.Address).IsRequired().HasMaxLength(250);

            Property(t => t.Deactive);
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn);
            Property(t => t.UpdatedBy);

            ToTable("People");
        }
    }
}
