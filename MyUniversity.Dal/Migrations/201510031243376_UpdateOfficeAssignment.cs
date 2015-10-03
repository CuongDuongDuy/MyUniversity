namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOfficeAssignment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OfficeAssignments", "WorkingHours", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.OfficeAssignments", "Phone", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficeAssignments", "Phone", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.OfficeAssignments", "WorkingHours", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
