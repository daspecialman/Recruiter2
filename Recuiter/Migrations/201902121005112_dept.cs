namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dept : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobViewModels", "Department_Id", "dbo.Departments");
            DropIndex("dbo.JobViewModels", new[] { "Department_Id" });
            DropColumn("dbo.JobViewModels", "Department_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobViewModels", "Department_Id", c => c.Int());
            CreateIndex("dbo.JobViewModels", "Department_Id");
            AddForeignKey("dbo.JobViewModels", "Department_Id", "dbo.Departments", "Id");
        }
    }
}
