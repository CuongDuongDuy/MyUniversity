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
            Map(t => t.CreatedOn).Not.Nullable();
            Map(t => t.CreatedBy).Not.Nullable();
            Map(t => t.UpdatedOn);
            Map(t => t.UpdatedBy);

            Map(t => t.DepartmentId).Not.Nullable();
            References(t => t.Department,"DepartmentId");
            
            Table("Courses");
            Schema("dbo");
        }
    }
}
