using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public class CourseMapping : ClassMap<Course>
    {
        public CourseMapping()
        {
            Id(t => t.Id).GeneratedBy.Guid();

            Map(t => t.Title).Not.Nullable().Length(250);
            Map(t => t.Credits).Not.Nullable();
            Map(t => t.Deactive);
            Map(t => t.CreatedOn);
            Map(t => t.CreatedBy);
            Map(t => t.UpdatedOn);
            Map(t => t.UpdatedBy);

            References(t => t.Department).Column("DepartmentId").Cascade.None();

            Version(x => x.RowVersion)
                .Generated.Always()
                .UnsavedValue("null")
                .CustomType("BinaryBlob")
                .Column("RowVersion")
                .CustomSqlType("timestamp")
                .Not.Nullable();
            
            Table("Courses");
            Schema("dbo");
        }
    }
}
