using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public class PersonMapping : ClassMap<Person>
    {
        public PersonMapping()
        {
            Id(t => t.Id).GeneratedBy.Guid();

            Map(t => t.IdentityNumber).Not.Nullable().Length(20);
            Map(t => t.FirstName).Not.Nullable().Length(50);
            Map(t => t.LastName).Not.Nullable().Length(50);
            Map(t => t.DateOfBirth).Not.Nullable();
            Map(t => t.Address).Not.Nullable().Length(250);

            Map(t => t.Deactive);
            Map(t => t.CreatedOn);
            Map(t => t.CreatedBy);
            Map(t => t.UpdatedOn);
            Map(t => t.UpdatedBy);

            Table("People");
            Schema("dbo");
        }
    }
}
