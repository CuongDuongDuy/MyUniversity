namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEnrollmentSetMarkNullable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfficeAssignments", "WorkingHours", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Enrollments", "Mark", c => c.Double());
            DropColumn("dbo.OfficeAssignments", "OpeningTime");
            DropColumn("dbo.OfficeAssignments", "ClosedTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OfficeAssignments", "ClosedTime", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.OfficeAssignments", "OpeningTime", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Enrollments", "Mark", c => c.Double(nullable: false));
            DropColumn("dbo.OfficeAssignments", "WorkingHours");
        }
    }
}
