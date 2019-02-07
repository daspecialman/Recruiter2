namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAppliedJobModelEdited : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicantAppliedJobs", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantAppliedJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.ApplicantAppliedJobs", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Applicants", "AppliedJob_Id", "dbo.ApplicantAppliedJobs");
            DropIndex("dbo.Applicants", new[] { "AppliedJob_Id" });
            DropIndex("dbo.ApplicantAppliedJobs", new[] { "ApplicantId" });
            DropIndex("dbo.ApplicantAppliedJobs", new[] { "JobId" });
            DropIndex("dbo.ApplicantAppliedJobs", new[] { "Applicant_Id" });
            DropColumn("dbo.Applicants", "AppliedJob_Id");
            DropTable("dbo.ApplicantAppliedJobs");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Applicants", "AppliedJob_Id", c => c.Int());
            CreateIndex("dbo.ApplicantAppliedJobs", "Applicant_Id");
            CreateIndex("dbo.ApplicantAppliedJobs", "JobId");
            CreateIndex("dbo.ApplicantAppliedJobs", "ApplicantId");
            CreateIndex("dbo.Applicants", "AppliedJob_Id");
            AddForeignKey("dbo.Applicants", "AppliedJob_Id", "dbo.ApplicantAppliedJobs", "Id");
            AddForeignKey("dbo.ApplicantAppliedJobs", "Applicant_Id", "dbo.Applicants", "Id");
            AddForeignKey("dbo.ApplicantAppliedJobs", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicantAppliedJobs", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
        }
    }
}
