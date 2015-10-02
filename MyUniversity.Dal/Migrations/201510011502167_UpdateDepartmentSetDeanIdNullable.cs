namespace MyUniversity.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDepartmentSetDeanIdNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Departments", new[] { "DeanId" });
            AlterColumn("dbo.Departments", "DeanId", c => c.Guid(nullable:true));
            CreateIndex("dbo.Departments", "DeanId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Departments", new[] { "DeanId" });
            AlterColumn("dbo.Departments", "DeanId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Departments", "DeanId");
        }
    }
}
