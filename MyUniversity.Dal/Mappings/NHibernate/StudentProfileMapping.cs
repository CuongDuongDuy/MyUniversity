using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public sealed class StudentProfileMapping : ClassMap<StudentProfile>
    {
        public StudentProfileMapping()
        {
            Table("StudentProfiles");
            Schema("dbo");

            //Id(x => x.).Column("Guid").GeneratedBy.Foreign("Student");
            Map(x => x.CreatedBy).Column("CreatedBy").Not.Nullable();
            Map(x => x.CreatedOn).Column("CreatedOn").Not.Nullable();
            Map(x => x.UpdatedBy).Column("UpdatedBy").Nullable();
            Map(x => x.UpdatedOn).Column("UpdatedOn").Nullable();
            Map(x => x.Deactive).Column("Deactive").Nullable();
            
            HasOne(x => x.Person).Constrained();
        }
    }
}
