namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationshipDepartmentOfficeAssignment : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OfficeAssignments", new[] { "DepartmentId" });
            DropPrimaryKey("dbo.OfficeAssignments");
            AddColumn("dbo.Departments", "OfficeAssignmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Courses", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.OfficeAssignments", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.OfficeAssignments", "DepartmentId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.OfficeAssignments", "Id");
            CreateIndex("dbo.Departments", "Id");
            AddForeignKey("dbo.Departments", "Id", "dbo.OfficeAssignments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Id", "dbo.OfficeAssignments");
            DropIndex("dbo.Departments", new[] { "Id" });
            DropPrimaryKey("dbo.OfficeAssignments");
            AlterColumn("dbo.OfficeAssignments", "DepartmentId", c => c.Guid());
            AlterColumn("dbo.OfficeAssignments", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Courses", "CreatedBy", c => c.String());
            DropColumn("dbo.Departments", "OfficeAssignmentId");
            AddPrimaryKey("dbo.OfficeAssignments", "Id");
            CreateIndex("dbo.OfficeAssignments", "DepartmentId");
        }
    }
}
