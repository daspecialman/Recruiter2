namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Monday11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "ExperienceLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Applicants", "JobTitle", c => c.String());
            AddColumn("dbo.Applicants", "Specialization", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "Specialization");
            DropColumn("dbo.Applicants", "JobTitle");
            DropColumn("dbo.Applicants", "ExperienceLevel");
        }
    }
}
