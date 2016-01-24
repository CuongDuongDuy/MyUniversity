using FluentNHibernate.Mapping;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.NHibernate
{
    public class EnrollmentMapping : ClassMap<Enrollment>
    {
        public EnrollmentMapping()
        {
            Id(t => t.Id).GeneratedBy.Guid();

            Map(t => t.CourseId).Not.Nullable();
            Map(t => t.StudentProfileId).Not.Nullable();
            Map(t => t.InstructorProfileId).Not.Nullable();
            Map(t => t.Mark).Nullable();
            Map(t => t.Deactive);
            Map(t => t.CreatedOn).Nullable();
            Map(t => t.CreatedBy).Nullable();
            Map(t => t.UpdatedOn);
            Map(t => t.UpdatedBy);

            References(t => t.Course).Column("CourseId").Cascade.None();
            References(t => t.StudentProfile).Column("StudentProfileId").Cascade.None();
            References(t => t.InstructorProfile).Column("InstructorProfileId").Cascade.None();

            Version(x => x.RowVersion)
                .Generated.Always()
                .UnsavedValue("null")
                .CustomType("BinaryBlob")
                .Column("RowVersion")
                .CustomSqlType("timestamp")
                .Not.Nullable();

            Table("Enrollments");
            Schema("dbo");
        }
    }
}
