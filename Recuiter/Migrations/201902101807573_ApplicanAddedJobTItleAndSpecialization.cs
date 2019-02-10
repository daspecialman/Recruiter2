namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicanAddedJobTItleAndSpecialization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "JobTitle", c => c.String());
            AddColumn("dbo.Applicants", "SpecialiZation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "SpecialiZation");
            DropColumn("dbo.Applicants", "JobTitle");
        }
    }
}
