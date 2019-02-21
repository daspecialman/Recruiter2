namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Wednesday : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Educations", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Skills", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Experiences", "Applicant_Id", "dbo.Applicants");
            DropIndex("dbo.Educations", new[] { "Applicant_Id" });
            DropIndex("dbo.Skills", new[] { "Applicant_Id" });
            DropIndex("dbo.Experiences", new[] { "Applicant_Id" });
            RenameColumn(table: "dbo.Educations", name: "Applicant_Id", newName: "ApplicantId");
            RenameColumn(table: "dbo.Skills", name: "Applicant_Id", newName: "ApplicantId");
            RenameColumn(table: "dbo.Experiences", name: "Applicant_Id", newName: "ApplicantId");
            AddColumn("dbo.Users", "ApplicantId", c => c.Int());
            AddColumn("dbo.Skills", "SkillTitle", c => c.String());
            AlterColumn("dbo.Educations", "ApplicantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "ApplicantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Experiences", "ApplicantId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "ApplicantId");
            CreateIndex("dbo.Educations", "ApplicantId");
            CreateIndex("dbo.Skills", "ApplicantId");
            CreateIndex("dbo.Experiences", "ApplicantId");
            AddForeignKey("dbo.Users", "ApplicantId", "dbo.Applicants", "Id");
            AddForeignKey("dbo.Educations", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Skills", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Experiences", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Experiences", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Skills", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Educations", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Users", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Experiences", new[] { "ApplicantId" });
            DropIndex("dbo.Skills", new[] { "ApplicantId" });
            DropIndex("dbo.Educations", new[] { "ApplicantId" });
            DropIndex("dbo.Users", new[] { "ApplicantId" });
            AlterColumn("dbo.Experiences", "ApplicantId", c => c.Int());
            AlterColumn("dbo.Skills", "ApplicantId", c => c.Int());
            AlterColumn("dbo.Educations", "ApplicantId", c => c.Int());
            DropColumn("dbo.Skills", "SkillTitle");
            DropColumn("dbo.Users", "ApplicantId");
            RenameColumn(table: "dbo.Experiences", name: "ApplicantId", newName: "Applicant_Id");
            RenameColumn(table: "dbo.Skills", name: "ApplicantId", newName: "Applicant_Id");
            RenameColumn(table: "dbo.Educations", name: "ApplicantId", newName: "Applicant_Id");
            CreateIndex("dbo.Experiences", "Applicant_Id");
            CreateIndex("dbo.Skills", "Applicant_Id");
            CreateIndex("dbo.Educations", "Applicant_Id");
            AddForeignKey("dbo.Experiences", "Applicant_Id", "dbo.Applicants", "Id");
            AddForeignKey("dbo.Skills", "Applicant_Id", "dbo.Applicants", "Id");
            AddForeignKey("dbo.Educations", "Applicant_Id", "dbo.Applicants", "Id");
        }
    }
}
