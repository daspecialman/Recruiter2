namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class today : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Department_Id", c => c.Int());
            AddColumn("dbo.JobViewModels", "Department_Id", c => c.Int());
            CreateIndex("dbo.Jobs", "Department_Id");
            CreateIndex("dbo.JobViewModels", "Department_Id");
            AddForeignKey("dbo.Jobs", "Department_Id", "dbo.Departments", "Id");
            AddForeignKey("dbo.JobViewModels", "Department_Id", "dbo.Departments", "Id");
            DropColumn("dbo.JobViewModels", "DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobViewModels", "DepartmentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.JobViewModels", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Jobs", "Department_Id", "dbo.Departments");
            DropIndex("dbo.JobViewModels", new[] { "Department_Id" });
            DropIndex("dbo.Jobs", new[] { "Department_Id" });
            DropColumn("dbo.JobViewModels", "Department_Id");
            DropColumn("dbo.Jobs", "Department_Id");
        }
    }
}
