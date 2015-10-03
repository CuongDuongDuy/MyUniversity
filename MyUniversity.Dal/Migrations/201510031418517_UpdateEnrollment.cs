namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEnrollment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseInstructor", "CouseId", "dbo.Courses");
            DropForeignKey("dbo.CourseInstructor", "InstructorId", "dbo.InstructorProfiles");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseInstructor", new[] { "CouseId" });
            DropIndex("dbo.CourseInstructor", new[] { "InstructorId" });
            AddColumn("dbo.Enrollments", "InstructorProfileId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Enrollments", "InstructorProfileId");
            AddForeignKey("dbo.Enrollments", "InstructorProfileId", "dbo.InstructorProfiles", "Id");
            AddForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses", "Id");
            DropTable("dbo.CourseInstructor");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CouseId = c.Guid(nullable: false),
                        InstructorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CouseId, t.InstructorId });
            
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Enrollments", "InstructorProfileId", "dbo.InstructorProfiles");
            DropIndex("dbo.Enrollments", new[] { "InstructorProfileId" });
            DropColumn("dbo.Enrollments", "InstructorProfileId");
            CreateIndex("dbo.CourseInstructor", "InstructorId");
            CreateIndex("dbo.CourseInstructor", "CouseId");
            AddForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CourseInstructor", "InstructorId", "dbo.InstructorProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CourseInstructor", "CouseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
