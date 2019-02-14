namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jQuery : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Educations", "Applicant_Id", "dbo.Applicants");
            DropIndex("dbo.Educations", new[] { "Applicant_Id" });
            RenameColumn(table: "dbo.Educations", name: "Applicant_Id", newName: "ApplicantId");
            AddColumn("dbo.Users", "ApplicantId", c => c.Int());
            AlterColumn("dbo.Educations", "ApplicantId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "ApplicantId");
            CreateIndex("dbo.Educations", "ApplicantId");
            AddForeignKey("dbo.Users", "ApplicantId", "dbo.Applicants", "Id");
            AddForeignKey("dbo.Educations", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Educations", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Users", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Educations", new[] { "ApplicantId" });
            DropIndex("dbo.Users", new[] { "ApplicantId" });
            AlterColumn("dbo.Educations", "ApplicantId", c => c.Int());
            DropColumn("dbo.Users", "ApplicantId");
            RenameColumn(table: "dbo.Educations", name: "ApplicantId", newName: "Applicant_Id");
            CreateIndex("dbo.Educations", "Applicant_Id");
            AddForeignKey("dbo.Educations", "Applicant_Id", "dbo.Applicants", "Id");
        }
    }
}
