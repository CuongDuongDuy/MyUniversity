using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public class DepartmentMapping : ClassMap<Department>
    {
        public DepartmentMapping()
        {
            Id(t => t.Id).GeneratedBy.Guid();

            Map(t => t.Name).Not.Nullable().Length(100);
            Map(t => t.StartDate).Not.Nullable();
            Map(t => t.DeanId).Nullable();
            Map(t => t.Deactive).Nullable();
            Map(t => t.CreatedOn).Not.Nullable();
            Map(t => t.CreatedBy).Not.Nullable();
            Map(t => t.UpdatedOn).Nullable();
            Map(t => t.UpdatedBy).Nullable();

            References(t => t.Dean).Column("DeanId");

            Table("Departments");
            Schema("dbo");
        }
    }
}
