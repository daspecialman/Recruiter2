namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAppliedJobModelUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantAppliedJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        JobStatus = c.Int(nullable: false),
                        Applicant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Applicants", t => t.Applicant_Id)
                .Index(t => t.ApplicantId)
                .Index(t => t.JobId)
                .Index(t => t.Applicant_Id);
            
            AddColumn("dbo.Applicants", "AppliedJob_Id", c => c.Int());
            AddColumn("dbo.Jobs", "JobStatus", c => c.Int(nullable: false));
            CreateIndex("dbo.Applicants", "AppliedJob_Id");
            AddForeignKey("dbo.Applicants", "AppliedJob_Id", "dbo.ApplicantAppliedJobs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "AppliedJob_Id", "dbo.ApplicantAppliedJobs");
            DropForeignKey("dbo.ApplicantAppliedJobs", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantAppliedJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.ApplicantAppliedJobs", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.ApplicantAppliedJobs", new[] { "Applicant_Id" });
            DropIndex("dbo.ApplicantAppliedJobs", new[] { "JobId" });
            DropIndex("dbo.ApplicantAppliedJobs", new[] { "ApplicantId" });
            DropIndex("dbo.Applicants", new[] { "AppliedJob_Id" });
            DropColumn("dbo.Jobs", "JobStatus");
            DropColumn("dbo.Applicants", "AppliedJob_Id");
            DropTable("dbo.ApplicantAppliedJobs");
        }
    }
}
