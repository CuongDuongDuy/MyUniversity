namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDepartmentOfficeAssignments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "CreatedBy", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "CreatedBy", c => c.String());
        }
    }
}
