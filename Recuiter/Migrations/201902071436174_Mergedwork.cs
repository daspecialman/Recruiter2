namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mergedwork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "JobTitle", c => c.String());
            AddColumn("dbo.Applications", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "Date");
            DropColumn("dbo.Applications", "JobTitle");
        }
    }
}
