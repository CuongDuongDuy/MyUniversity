using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
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
        public DbSet<InstructorProfile> InstructorProfile { get; set; }
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

            var ctx = ((IObjectContextAdapter)this).ObjectContext;

            var objectStateEntryList = ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added
                                                                                    | EntityState.Modified
                                                                                    | EntityState.Deleted)
                .ToList();

            foreach (var entry in objectStateEntryList)
            {
                var entity = entry.Entity;
                if (entry.IsRelationship || entity == null || entity.GetType() == typeof(AuditTrail))
                {
                    continue;
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.GetType().GetProperty("Deactive").SetValue(entity, false, null);
                        entity.GetType().GetProperty("CreatedBy").SetValue(entity, EntityConstant.CreatedBy, null);
                        entity.GetType().GetProperty("CreatedOn").SetValue(entity, DateTime.UtcNow, null);

                        //AuditTrails.Add(new AuditTrail(AuditTrailActionType.Added, JsonConvert.SerializeObject(entity)));
                        break;
                    case EntityState.Deleted:
                        entity.GetType().GetProperty("Deactive").SetValue(entity, true, null);
                        entity.GetType().GetProperty("UpdatedBy").SetValue(entity, EntityConstant.UpdatedBy, null);
                        entity.GetType().GetProperty("UpdatedOn").SetValue(entity, DateTime.UtcNow, null);

                        AuditTrails.Add(new AuditTrail(AuditTrailActionType.Delete, JsonConvert.SerializeObject(entity)));
                        break;
                    case EntityState.Modified:
                        entity.GetType().GetProperty("UpdatedBy").SetValue(entity, EntityConstant.UpdatedBy, null);
                        entity.GetType().GetProperty("UpdatedOn").SetValue(entity, DateTime.UtcNow, null);

                        //AuditTrails.Add(new AuditTrail(AuditTrailActionType.Modified, JsonConvert.SerializeObject(entity)));
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
            var departments = new List<Department>
            {
                new Department
                {
                    Name = "English",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignment = 
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 101, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        }
                },
                new Department
                {
                    Name = "Mathematics",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignment = 
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 102, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        }
                },
                new Department
                {
                    Name = "Economics",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignment = 
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 104, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        }
                },
                new Department
                {
                    Name = "Engineering",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignment = new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 109, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        }
                }
            };

            departments.ForEach(d => context.Departments.AddOrUpdate(d));
            context.SaveChanges();

            var students = new List<StudentProfile>
            {
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "012345678",
                        FirstName = "Carson",
                        LastName = "Alexander",
                        Address = "1 Tran Hung Dao, q.1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-12-20")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Engineering").Id
                },
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "0123458679",
                        FirstName = "Meredith",
                        LastName = "Alonso",
                        Address = "1/2 Nguyen Thi Minh Khai, q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-01-01")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Mathematics").Id
                },
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "0123458679",
                        FirstName = "Arturo",
                        LastName = "Anand",
                        Address = "23A Nguyen Thi Minh Khai, q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-03-20")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Mathematics").Id
                },
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "0123458892",
                        FirstName = "Gytis",
                        LastName = "Barzdukas",
                        Address = "23A Tran Hung Dao, q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-02-23")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Engineering").Id
                },
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "0123458812",
                        FirstName = "Yan",
                        LastName = "Li",
                        Address = "350 Tran Hung Dao, q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-03-27")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Engineering").Id
                },
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "0123458855",
                        FirstName = "Peggy",
                        LastName = "Justice",
                        Address = "358 Ngo Gia Tu, q5, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-07-27")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Economics").Id
                },
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "0123458888",
                        FirstName = "Laura",
                        LastName = "Norman",
                        Address = "470 Tran Hung Dao, q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-07-04")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Economics").Id
                },
                new StudentProfile
                {
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "0123458823",
                        FirstName = "Nino",
                        LastName = "Olivetto",
                        Address = "23 Bui Minh Truc, q8, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1980-09-09")
                    },
                    DepartmentId = departments.Single(x => x.Name == "English").Id
                }
            };
            students.ForEach(s => context.StudentProfiles.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var instructors = new List<InstructorProfile>
            {
                new InstructorProfile
                {
                    HireDate = DateTime.Parse("2010-09-01"),
                    EffectiveDate = DateTime.Parse("2010-09-01"),
                    ExpiryDate = DateTime.Parse("2014-09-01"),
                    Person = new Person
                    {
                        IdentityNumber = "2123489833",
                        FirstName = "Kim",
                        LastName = "Abercrombie",
                        Address = "3 Hoang Minh Dao, q8, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1970-12-12")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Engineering").Id

                },
                new InstructorProfile
                {
                    HireDate = DateTime.Parse("2002-07-06"),
                    EffectiveDate = DateTime.Parse("2002-07-06"),
                    ExpiryDate = DateTime.Parse("2022-07-06"),
                    Person = new Person
                    {
                        IdentityNumber = "2123482837",
                        FirstName = "Fadi",
                        LastName = "Fakhouri",
                        Address = "23 Hoang Van Thu, q. Tan Binh, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1969-06-23")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Economics").Id
                },
                new InstructorProfile
                {
                    HireDate = DateTime.Parse("2002-07-06"),
                    EffectiveDate = DateTime.Parse("2002-07-06"),
                    Person = new Person
                    {
                        IdentityNumber = "2123532837",
                        FirstName = "Roger",
                        LastName = "Harui",
                        Address = "45/34 Thu Khoa Huan q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1970-05-03")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Mathematics").Id
                },
                new InstructorProfile
                {
                    HireDate = DateTime.Parse("2002-07-06"),
                    EffectiveDate = DateTime.Parse("2002-07-06"),
                    Person = new Person
                    {
                        IdentityNumber = "2123532521",
                        FirstName = "Roger",
                        LastName = "Kapoor",
                        Address = "45/34 Thu Khoa Huan q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1970-05-03")
                    },
                    DepartmentId = departments.Single(x => x.Name == "Economics").Id
                },
                new InstructorProfile
                {
                    HireDate = DateTime.Parse("2002-07-06"),
                    EffectiveDate = DateTime.Parse("2002-10-10"),
                    ExpiryDate = DateTime.Parse("2012-10-10"),
                    Person = new Person
                    {
                        IdentityNumber = "2123532521",
                        FirstName = "Roger",
                        LastName = "Zheng",
                        Address = "34/34  Cach Mang Thang Tam, q1, Ho Chi Minh city",
                        DateOfBirth = DateTime.Parse("1970-05-03")
                    },
                    DepartmentId = departments.Single(x => x.Name == "English").Id
                }
            };
            instructors.ForEach(s => context.InstructorProfile.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course
                {
                    Title = "Chemistry",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "Engineering").Id
                },
                new Course
                {
                    Title = "Microeconomics",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "Economics").Id,
                },
                new Course
                {
                    Title = "Macroeconomics",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "Economics").Id,
                },
                new Course
                {
                    Title = "Calculus",
                    Credits = 4,
                    DepartmentId = departments.Single(s => s.Name == "Mathematics").Id,
                },
                new Course
                {
                    Title = "Trigonometry",
                    Credits = 4,
                    DepartmentId = departments.Single(s => s.Name == "Mathematics").Id
                },
                new Course
                {
                    Title = "Composition",
                    Credits = 3,
                    DepartmentId = departments.Single(s => s.Name == "English").Id
                },
                new Course
                {
                    Title = "Literature",
                    Credits = 4,
                    DepartmentId = departments.Single(s => s.Name == "English").Id,
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Alexander").Id,
                    CourseId = courses.Single(c => c.Title == "Chemistry").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Abercrombie").Id,
                    Mark = 8.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Alexander").Id,
                    CourseId = courses.Single(c => c.Title == "Microeconomics").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Abercrombie").Id,
                    Mark = 9.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Alexander").Id,
                    CourseId = courses.Single(c => c.Title == "Macroeconomics").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Fakhouri").Id,
                    Mark = 9.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Alonso").Id,
                    CourseId = courses.Single(c => c.Title == "Calculus").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Fakhouri").Id,
                    Mark = 8.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Alonso").Id,
                    CourseId = courses.Single(c => c.Title == "Trigonometry").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Harui").Id,
                    Mark = 7.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Alonso").Id,
                    CourseId = courses.Single(c => c.Title == "Composition").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Harui").Id,
                    Mark = 8.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Anand").Id,
                    CourseId = courses.Single(c => c.Title == "Composition").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Harui").Id,
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Anand").Id,
                    CourseId = courses.Single(c => c.Title == "Microeconomics").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Kapoor").Id,
                    Mark = 8.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Barzdukas").Id,
                    CourseId = courses.Single(c => c.Title == "Chemistry").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Kapoor").Id
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Li").Id,
                    CourseId = courses.Single(c => c.Title == "Composition").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Kapoor").Id,
                    Mark = 10.00
                },
                new Enrollment
                {
                    StudentProfileId = students.Single(s => s.Person.LastName == "Justice").Id,
                    CourseId = courses.Single(c => c.Title == "Literature").Id,
                    InstructorProfileId = instructors.Single(i=>i.Person.LastName == "Zheng").Id,
                    Mark = 10.00
                }
            };
            enrollments.ForEach(e => context.Enrollments.AddOrUpdate(e));
            context.SaveChanges();
        }
    }
}
