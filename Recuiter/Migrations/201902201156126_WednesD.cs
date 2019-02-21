namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WednesD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobViewModels", "QualificationFormat", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobViewModels", "QualificationFormat");
        }
    }
}
