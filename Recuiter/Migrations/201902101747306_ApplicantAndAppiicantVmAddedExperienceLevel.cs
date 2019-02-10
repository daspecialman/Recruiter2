namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicantAndAppiicantVmAddedExperienceLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "ExperienceLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "ExperienceLevel");
        }
    }
}
