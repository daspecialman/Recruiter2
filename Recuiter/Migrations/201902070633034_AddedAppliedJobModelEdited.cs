namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAppliedJobModelEdited : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "JobStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "JobStatus", c => c.Int(nullable: false));
        }
    }
}
