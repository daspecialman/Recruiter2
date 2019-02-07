namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Images", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Images", "UserId", "dbo.Users");
            DropIndex("dbo.Images", new[] { "UserId" });
            DropIndex("dbo.Images", new[] { "CreatedById" });
            DropIndex("dbo.Images", new[] { "LastModifiedById" });
            AddColumn("dbo.ApplicantDocuments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicants", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "ImagePath", c => c.String());
            AddColumn("dbo.Departments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicantReviewAssessments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationReviews", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applications", "JobTitle", c => c.String());
            AddColumn("dbo.Applications", "Date", c => c.DateTime());
            AddColumn("dbo.Applications", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.InterviewQuestions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReviewResults", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicantDocuments", "IsDeleted");
            DropColumn("dbo.Applicants", "IsDeleted");
            DropColumn("dbo.Departments", "IsDeleted");
            DropColumn("dbo.UserRoles", "IsDeleted");
            DropColumn("dbo.Roles", "IsDeleted");
            DropColumn("dbo.ApplicantReviewAssessments", "IsDeleted");
            DropColumn("dbo.ApplicationReviews", "IsDeleted");
            DropColumn("dbo.Applications", "IsDeleted");
            DropColumn("dbo.Jobs", "IsDeleted");
            DropColumn("dbo.InterviewQuestions", "IsDeleted");
            DropColumn("dbo.ReviewResults", "IsDeleted");
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UserId = c.Int(nullable: false),
                        ImagePath = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ReviewResults", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.InterviewQuestions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applications", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationReviews", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicantReviewAssessments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRoles", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Departments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicants", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicantDocuments", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.ReviewResults", "IsActive");
            DropColumn("dbo.InterviewQuestions", "IsActive");
            DropColumn("dbo.Jobs", "IsActive");
            DropColumn("dbo.Applications", "IsActive");
            DropColumn("dbo.Applications", "Date");
            DropColumn("dbo.Applications", "JobTitle");
            DropColumn("dbo.ApplicationReviews", "IsActive");
            DropColumn("dbo.ApplicantReviewAssessments", "IsActive");
            DropColumn("dbo.Roles", "IsActive");
            DropColumn("dbo.UserRoles", "IsActive");
            DropColumn("dbo.Departments", "IsActive");
            DropColumn("dbo.Users", "ImagePath");
            DropColumn("dbo.Applicants", "IsActive");
            DropColumn("dbo.ApplicantDocuments", "IsActive");
            CreateIndex("dbo.Images", "LastModifiedById");
            CreateIndex("dbo.Images", "CreatedById");
            CreateIndex("dbo.Images", "UserId");
            AddForeignKey("dbo.Images", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Images", "LastModifiedById", "dbo.Users", "Id");
            AddForeignKey("dbo.Images", "CreatedById", "dbo.Users", "Id");
        }
    }
}
