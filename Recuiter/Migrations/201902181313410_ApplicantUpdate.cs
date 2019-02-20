namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicantUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Experiences", "Applicant_Id", "dbo.Applicants");
            DropIndex("dbo.Skills", new[] { "Applicant_Id" });
            DropIndex("dbo.Experiences", new[] { "Applicant_Id" });
            RenameColumn(table: "dbo.Skills", name: "Applicant_Id", newName: "ApplicantId");
            RenameColumn(table: "dbo.Experiences", name: "Applicant_Id", newName: "ApplicantId");
            AlterColumn("dbo.Skills", "ApplicantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Experiences", "ApplicantId", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "ApplicantId");
            CreateIndex("dbo.Experiences", "ApplicantId");
            AddForeignKey("dbo.Skills", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Experiences", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Experiences", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Skills", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Experiences", new[] { "ApplicantId" });
            DropIndex("dbo.Skills", new[] { "ApplicantId" });
            AlterColumn("dbo.Experiences", "ApplicantId", c => c.Int());
            AlterColumn("dbo.Skills", "ApplicantId", c => c.Int());
            RenameColumn(table: "dbo.Experiences", name: "ApplicantId", newName: "Applicant_Id");
            RenameColumn(table: "dbo.Skills", name: "ApplicantId", newName: "Applicant_Id");
            CreateIndex("dbo.Experiences", "Applicant_Id");
            CreateIndex("dbo.Skills", "Applicant_Id");
            AddForeignKey("dbo.Experiences", "Applicant_Id", "dbo.Applicants", "Id");
            AddForeignKey("dbo.Skills", "Applicant_Id", "dbo.Applicants", "Id");
        }
    }
}
