﻿using FluentNHibernate.Mapping;
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
            Map(t => t.CreatedOn).Not.Nullable();
            Map(t => t.CreatedBy).Not.Nullable();
            Map(t => t.UpdatedOn);
            Map(t => t.UpdatedBy);

            Table("Enrollments");
            Schema("dbo");
        }
    }
}