﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Mappings.EntityFramework
{
    public class CourseMapping : EntityTypeConfiguration<Course>
    {
        public CourseMapping()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Title).IsRequired().HasMaxLength(250);
            Property(t => t.Credits).IsRequired();
            Property(t => t.DepartmentId).IsRequired();
            Property(t => t.Deactive);
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn);
            Property(t => t.UpdatedBy);

            HasRequired(e => e.Department)
                .WithMany(e => e.Courses)
                .HasForeignKey(t => t.DepartmentId)
                .WillCascadeOnDelete(false);

            ToTable("Courses");
        }
    }
}
