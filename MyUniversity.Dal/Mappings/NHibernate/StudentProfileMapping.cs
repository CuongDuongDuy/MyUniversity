using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public sealed class StudentProfileMapping : ClassMap<StudentProfile>
    {
        public StudentProfileMapping()
        {
            Id(t => t.Id).GeneratedBy.Guid();

            Map(t => t.EnrollmentDate).Not.Nullable();

            Map(t => t.EffectiveDate).Not.Nullable();
            Map(t => t.ExpiryDate).Nullable();

            Map(t => t.CreatedBy).Column("CreatedBy");
            Map(t => t.CreatedOn).Column("CreatedOn");
            Map(t => t.UpdatedBy).Column("UpdatedBy");
            Map(t => t.UpdatedOn).Column("UpdatedOn");
            Map(t => t.Deactive).Column("Deactive");

            References(t => t.Department, "DepartmentId").Cascade.All();
            References(t => t.Person,"PersonId").Cascade.All();

            Table("StudentProfiles");
            Schema("dbo");
        }
    }
}
