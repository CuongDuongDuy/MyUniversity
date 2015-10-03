namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentInstructorSetExpiryDateNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OfficeAssignments", new[] { "DepartmentId" });
            DropIndex("dbo.StudentProfiles", new[] { "DepartmentId" });
            AlterColumn("dbo.StudentProfiles", "DepartmentId", c => c.Guid());
            AlterColumn("dbo.OfficeAssignments", "DepartmentId", c => c.Guid());
            CreateIndex("dbo.OfficeAssignments", "DepartmentId");
            CreateIndex("dbo.StudentProfiles", "DepartmentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StudentProfiles", new[] { "DepartmentId" });
            DropIndex("dbo.OfficeAssignments", new[] { "DepartmentId" });
            AlterColumn("dbo.OfficeAssignments", "DepartmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.StudentProfiles", "DepartmentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.StudentProfiles", "DepartmentId");
            CreateIndex("dbo.OfficeAssignments", "DepartmentId");
        }
    }
}
