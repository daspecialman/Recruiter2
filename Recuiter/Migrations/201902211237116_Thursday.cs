namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thursday : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Jobs", name: "Department_Id", newName: "DepartmentId");
            RenameIndex(table: "dbo.Jobs", name: "IX_Department_Id", newName: "IX_DepartmentId");
            AddColumn("dbo.Applicants", "ApplicantId", c => c.Int(nullable: false));
            AddColumn("dbo.Applicants", "Job_Id", c => c.Int());
            CreateIndex("dbo.Applicants", "Job_Id");
            AddForeignKey("dbo.Applicants", "Job_Id", "dbo.Jobs", "Id");
            DropTable("dbo.JobViewModels");
        }
        
        public override void Down()
        {
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
                        QualificationFormat = c.String(),
                        ContractFormat = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Applicants", "Job_Id", "dbo.Jobs");
            DropIndex("dbo.Applicants", new[] { "Job_Id" });
            DropColumn("dbo.Applicants", "Job_Id");
            DropColumn("dbo.Applicants", "ApplicantId");
            RenameIndex(table: "dbo.Jobs", name: "IX_DepartmentId", newName: "IX_Department_Id");
            RenameColumn(table: "dbo.Jobs", name: "DepartmentId", newName: "Department_Id");
        }
    }
}
