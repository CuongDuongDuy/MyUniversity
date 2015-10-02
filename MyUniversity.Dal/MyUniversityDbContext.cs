using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Mappings.EntityFramework;
using Newtonsoft.Json;

namespace MyUniversity.Dal
{
    public class MyUniversityDbContext : DbContext
    {
        #region Entities

        public DbSet<Person> Persons { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<InstructorProfile> TeacherProfiles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }

        #endregion

        public MyUniversityDbContext()
            : base("name=MyUniversityDb")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMapping());
            modelBuilder.Configurations.Add(new StudentProfileMapping());
            modelBuilder.Configurations.Add(new InstructorProfileMapping());
            modelBuilder.Configurations.Add(new DepartmentMapping());
            modelBuilder.Configurations.Add(new OfficeAssignmentMapping());
            modelBuilder.Configurations.Add(new CourseMapping());
            modelBuilder.Configurations.Add(new EnrollmentMapping());
            modelBuilder.Configurations.Add(new AuditTrailMapping());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var ctx = ((IObjectContextAdapter) this).ObjectContext;

            var objectStateEntryList = ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added
                                                                                    | EntityState.Modified
                                                                                    | EntityState.Deleted)
                .ToList();

            foreach (var entry in objectStateEntryList)
            {
                var entity = entry.Entity;
                if (entry.IsRelationship || entity == null || entity.GetType() == typeof (AuditTrail))
                {
                    continue;
                }
                switch (entry.State)
                {
                    case EntityState.Added:

                        entity.GetType().GetProperty("Deactive").SetValue(entity, false, null);
                        entity.GetType().GetProperty("CreatedBy").SetValue(entity, EntityConstant.CreatedBy, null);
                        entity.GetType().GetProperty("CreatedOn").SetValue(entity, DateTime.UtcNow, null);

                        AuditTrails.Add(new AuditTrail(AuditTrailActionType.Added, JsonConvert.SerializeObject(entity)));
                        break;
                    case EntityState.Deleted:
                        entity.GetType().GetProperty("Deactive").SetValue(entity, true, null);
                        entity.GetType().GetProperty("UpdatedBy").SetValue(entity, EntityConstant.UpdatedBy, null);
                        entity.GetType().GetProperty("UpdatedOn").SetValue(entity, DateTime.UtcNow, null);

                        AuditTrails.Add(new AuditTrail(AuditTrailActionType.Modified,
                            JsonConvert.SerializeObject(entity)));
                        break;
                    case EntityState.Modified:
                        entity.GetType().GetProperty("UpdatedBy").SetValue(entity, EntityConstant.UpdatedBy, null);
                        entity.GetType().GetProperty("UpdatedOn").SetValue(entity, DateTime.UtcNow, null);

                        AuditTrails.Add(new AuditTrail(AuditTrailActionType.Modified,
                            JsonConvert.SerializeObject(entity)));
                        break;
                }
            }
            return base.SaveChanges();
        }
    }

    public class MyUniversityDbInitializer : DropCreateDatabaseAlways<MyUniversityDbContext>
    {
        protected override void Seed(MyUniversityDbContext context)
        {
            var students = new List<Student>
            {
                new Student
                {
                    FirstMidName = "Carson",
                    LastName =
                        "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01")
                },
                new Student
                {
                    FirstMidName = "Meredith",
                    LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01")
                },
                new Student
                {
                    FirstMidName = "Arturo",
                    LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01")
                },
                new Student
                {
                    FirstMidName = "Gytis",
                    LastName =
                        "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01")
                },
                new Student
                {
                    FirstMidName = "Yan",
                    LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01")
                },
                new Student
                {
                    FirstMidName = "Peggy",
                    LastName =
                        "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01")
                },
                new Student
                {
                    FirstMidName = "Laura",
                    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01")
                },
                new Student
                {
                    FirstMidName = "Nino",
                    LastName =
                        "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01")
                }
            }
                ;
            students.ForEach(s => context.Students.AddOrUpdate(p =>
                p.LastName, s));
            context.SaveChanges();
            var instructors = new List<Instructor>
            {
                new Instructor
                {
                    FirstMidName = "Kim",
                    LastName =
                        "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11")
                },
                new Instructor
                {
                    FirstMidName = "Fadi",
                    LastName =
                        "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06")
                },
                new Instructor
                {
                    FirstMidName = "Roger",
                    LastName =
                        "Harui",
                    HireDate = DateTime.Parse("1998-07-01")
                },
                new Instructor
                {
                    FirstMidName = "Candace",
                    LastName =
                        "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15")
                },
                new Instructor
                {
                    FirstMidName = "Roger",
                    LastName =
                        "Zheng",
                    HireDate = DateTime.Parse("2004-02-12")
                }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p =>
                p.LastName, s));
            context.SaveChanges();
        }
    }
}
