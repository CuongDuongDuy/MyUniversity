namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForeignKeys : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TeacherProfiles", newName: "InstructorProfiles");
            DropIndex("dbo.Courses", new[] { "Department_Id" });
            DropIndex("dbo.StudentProfiles", new[] { "Department_Id" });
            DropIndex("dbo.InstructorProfiles", new[] { "Department_Id" });
            DropColumn("dbo.Courses", "DepartmentId");
            RenameColumn(table: "dbo.Courses", name: "Department_Id", newName: "DepartmentId");
            RenameColumn(table: "dbo.Departments", name: "Dean_Id", newName: "DeanId");
            RenameColumn(table: "dbo.InstructorProfiles", name: "Department_Id", newName: "DepartmentId");
            RenameColumn(table: "dbo.StudentProfiles", name: "Department_Id", newName: "DepartmentId");
            RenameIndex(table: "dbo.Departments", name: "IX_Dean_Id", newName: "IX_DeanId");
            AlterColumn("dbo.Courses", "DepartmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Courses", "DepartmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.InstructorProfiles", "DepartmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.StudentProfiles", "DepartmentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Courses", "DepartmentId");
            CreateIndex("dbo.StudentProfiles", "DepartmentId");
            CreateIndex("dbo.InstructorProfiles", "DepartmentId");
            DropColumn("dbo.Departments", "InstructorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "InstructorId", c => c.Guid());
            DropIndex("dbo.InstructorProfiles", new[] { "DepartmentId" });
            DropIndex("dbo.StudentProfiles", new[] { "DepartmentId" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            AlterColumn("dbo.StudentProfiles", "DepartmentId", c => c.Guid());
            AlterColumn("dbo.InstructorProfiles", "DepartmentId", c => c.Guid());
            AlterColumn("dbo.Courses", "DepartmentId", c => c.Guid());
            AlterColumn("dbo.Courses", "DepartmentId", c => c.Int());
            RenameIndex(table: "dbo.Departments", name: "IX_DeanId", newName: "IX_Dean_Id");
            RenameColumn(table: "dbo.StudentProfiles", name: "DepartmentId", newName: "Department_Id");
            RenameColumn(table: "dbo.InstructorProfiles", name: "DepartmentId", newName: "Department_Id");
            RenameColumn(table: "dbo.Departments", name: "DeanId", newName: "Dean_Id");
            RenameColumn(table: "dbo.Courses", name: "DepartmentId", newName: "Department_Id");
            AddColumn("dbo.Courses", "DepartmentId", c => c.Int());
            CreateIndex("dbo.InstructorProfiles", "Department_Id");
            CreateIndex("dbo.StudentProfiles", "Department_Id");
            CreateIndex("dbo.Courses", "Department_Id");
            RenameTable(name: "dbo.InstructorProfiles", newName: "TeacherProfiles");
        }
    }
}
