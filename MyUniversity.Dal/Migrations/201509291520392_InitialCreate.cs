namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditTrails",
                c => new
                    {
                        Guid = c.Guid(nullable: false, identity: true),
                        ActionType = c.String(nullable: false, maxLength: 50),
                        Value = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Credits = c.Double(nullable: false),
                        DepartmentId = c.Int(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Deactive = c.Boolean(),
                        Department_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        InstructorId = c.Guid(),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Deactive = c.Boolean(),
                        Dean_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeacherProfiles", t => t.Dean_Id)
                .Index(t => t.Dean_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IdentityNumber = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Deactive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfficeAssignments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Location = c.String(nullable: false, maxLength: 250),
                        OpeningTime = c.String(nullable: false, maxLength: 20),
                        ClosedTime = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 20),
                        DepartmentId = c.Guid(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Deactive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CourseId = c.Guid(nullable: false),
                        StudentProfileId = c.Guid(nullable: false),
                        Mark = c.Double(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Deactive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.StudentProfiles", t => t.StudentProfileId)
                .Index(t => t.CourseId)
                .Index(t => t.StudentProfileId);
            
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CouseId = c.Guid(nullable: false),
                        InstructorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CouseId, t.InstructorId })
                .ForeignKey("dbo.Courses", t => t.CouseId, cascadeDelete: true)
                .ForeignKey("dbo.TeacherProfiles", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.CouseId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.StudentProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Department_Id = c.Guid(),
                        EffectiveDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(),
                        PersonId = c.Guid(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Deactive = c.Boolean(),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.Department_Id)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.TeacherProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Department_Id = c.Guid(),
                        EffectiveDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(),
                        PersonId = c.Guid(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Deactive = c.Boolean(),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.Department_Id)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherProfiles", "PersonId", "dbo.People");
            DropForeignKey("dbo.TeacherProfiles", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.StudentProfiles", "PersonId", "dbo.People");
            DropForeignKey("dbo.StudentProfiles", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.CourseInstructor", "InstructorId", "dbo.TeacherProfiles");
            DropForeignKey("dbo.CourseInstructor", "CouseId", "dbo.Courses");
            DropForeignKey("dbo.Enrollments", "StudentProfileId", "dbo.StudentProfiles");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.OfficeAssignments", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Dean_Id", "dbo.TeacherProfiles");
            DropForeignKey("dbo.Courses", "Department_Id", "dbo.Departments");
            DropIndex("dbo.TeacherProfiles", new[] { "PersonId" });
            DropIndex("dbo.TeacherProfiles", new[] { "Department_Id" });
            DropIndex("dbo.StudentProfiles", new[] { "PersonId" });
            DropIndex("dbo.StudentProfiles", new[] { "Department_Id" });
            DropIndex("dbo.CourseInstructor", new[] { "InstructorId" });
            DropIndex("dbo.CourseInstructor", new[] { "CouseId" });
            DropIndex("dbo.Enrollments", new[] { "StudentProfileId" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.OfficeAssignments", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "Dean_Id" });
            DropIndex("dbo.Courses", new[] { "Department_Id" });
            DropTable("dbo.TeacherProfiles");
            DropTable("dbo.StudentProfiles");
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.Enrollments");
            DropTable("dbo.OfficeAssignments");
            DropTable("dbo.People");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.AuditTrails");
        }
    }
}
