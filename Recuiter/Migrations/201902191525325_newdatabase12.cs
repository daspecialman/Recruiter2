namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatabase12 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.JobViewModels");
            DropTable("dbo.PostedJobVMs");
            DropTable("dbo.TotalApplicantAppliedVMs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TotalApplicantAppliedVMs",
                c => new
                    {
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        Department = c.String(),
                        Email = c.String(),
                        JobTitle = c.String(),
                        PhoneNumber = c.String(),
                        DateApplied = c.DateTime(),
                    })
                .PrimaryKey(t => t.FirstName);
            
            CreateTable(
                "dbo.PostedJobVMs",
                c => new
                    {
                        Department = c.String(nullable: false, maxLength: 128),
                        ContractClass = c.Int(nullable: false),
                        Status = c.String(),
                        DatePosted = c.DateTime(),
                    })
                .PrimaryKey(t => t.Department);
            
            CreateTable(
                "dbo.JobViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Responsibility = c.String(nullable: false),
                        Characteristics = c.String(nullable: false),
                        SkillSet = c.Int(nullable: false),
                        MinimumQualification = c.Int(nullable: false),
                        ExperienceLevel = c.Int(nullable: false),
                        ExperienceLength = c.Int(nullable: false),
                        ContractClass = c.Int(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
