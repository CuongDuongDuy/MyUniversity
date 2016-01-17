using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public class OfficeAssignmentMapping : ClassMap<OfficeAssignment>
    {
        public OfficeAssignmentMapping()
        {
            Id(t => t.Id).GeneratedBy.Guid();

            Map(t => t.Location).Not.Nullable().Length(250);
            Map(t => t.WorkingHours).Not.Nullable().Length(100);
            Map(t => t.Phone).Not.Nullable().Length(50);
            Map(t => t.DepartmentId).Nullable();

            Map(t => t.Deactive).Nullable();
            Map(t => t.CreatedOn).Not.Nullable();
            Map(t => t.CreatedBy).Not.Nullable();
            Map(t => t.UpdatedOn).Nullable();
            Map(t => t.UpdatedBy).Nullable();

            References(t => t.Department).Column("DepartmentId").Cascade.None();
            
            Table("OfficeAssignments");
            Schema("dbo");
        }
    }
}
