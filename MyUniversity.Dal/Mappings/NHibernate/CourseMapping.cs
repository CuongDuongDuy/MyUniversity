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
            Map(t => t.DepartmentId).Not.Nullable();

            Table("Courses");
            Schema("dbo");
        }
    }
}
