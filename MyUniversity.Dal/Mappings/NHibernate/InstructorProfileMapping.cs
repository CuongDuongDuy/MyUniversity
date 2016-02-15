using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public sealed class InstructorProfileMapping : ClassMap<InstructorProfile>
    {
        public InstructorProfileMapping()
        {
            Id(t => t.Id).GeneratedBy.Guid();

            Map(t => t.HireDate).Not.Nullable();
            Map(t => t.EffectiveDate).Not.Nullable();
            Map(t => t.ExpiryDate).Nullable();

            Map(t => t.CreatedBy).Column("CreatedBy").Nullable();
            Map(t => t.CreatedOn).Column("CreatedOn").Nullable();
            Map(t => t.UpdatedBy).Column("UpdatedBy").Nullable();
            Map(t => t.UpdatedOn).Column("UpdatedOn").Nullable();
            Map(t => t.Deactive).Column("Deactive").Nullable();

            References(t => t.Department).Column("DepartmentId").Cascade.None();
            References(t => t.Person).Column("PersonId").Cascade.All();

            Version(x => x.RowVersion)
                .Generated.Always()
                .UnsavedValue("null")
                .CustomType("BinaryBlob")
                .Column("RowVersion")
                .CustomSqlType("timestamp")
                .Not.Nullable();

            Table("InstructorProfiles");
            Schema("dbo");
        }
    }
}
