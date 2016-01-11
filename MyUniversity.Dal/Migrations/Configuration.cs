using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyUniversityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyUniversityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            InitialDatabase(context);
        }

        public static void InitialDatabase(MyUniversityDbContext context)
        {
            var departments = new List<Department>
            {
                new Department
                {
                    Name = "English",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignments = new[]
                    {
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 101, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        }
                    }
                },
                new Department
                {
                    Name = "Mathematics",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignments = new[]
                    {
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 102, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        }
                    }
                },
                new Department
                {
                    Name = "Economics",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignments = new[]
                    {
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 104, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        }
                    }
                },
                new Department
                {
                    Name = "Engineering",
                    StartDate = DateTime.Parse("2010-01-01"),
                    OfficeAssignments = new[]
                    {
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "Rom 109, headquarter, 97 Vo Van Tan, q1, Ho Chi Minh city",
                            Phone = "38 908 957, 38 908 958"
                        },
                        new OfficeAssignment
                        {
                            WorkingHours = "9:00 - 11:30, 13:30 - 17:00, weekdays",
                            Location = "GFloor, 100 Dinh Tien Hoan, Binh Thanh, Ho Chi Minh city",
                            Phone = "39 897 888"
                        }
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
