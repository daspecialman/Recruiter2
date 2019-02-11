namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        Name = c.String(),
                        FilePath = c.String(),
                        Type = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.ApplicantId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        YearsOfExperience = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Languages = c.String(),
                        EducationLevel = c.Int(nullable: false),
                        Bio = c.String(),
                        Achievement = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        JobTitle = c.String(),
                        Date = c.DateTime(),
                        JobId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.ApplicantId)
                .Index(t => t.JobId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(),
                        LastModifiedById = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        Username = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ImagePath = c.String(),
                        Password = c.String(),
                        ProfilePictureUrl = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DepartmentId = c.Int(),
                        ResetPasswordCode = c.String(),
                        OldPassword = c.String(),
                        NewPassword = c.String(),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HoDId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.HoDId)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.HoDId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(nullable: false),
                        Role_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Jobs",
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
                        IsApproved = c.Boolean(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Code = c.String(),
                        Applicant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.Applicant_Id)
                .Index(t => t.Applicant_Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qualification = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Institution = c.String(),
                        CourseStudied = c.String(),
                        Applicant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.Applicant_Id)
                .Index(t => t.Applicant_Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Skilllevel = c.Int(nullable: false),
                        Achievement = c.String(),
                        Applicant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.Applicant_Id)
                .Index(t => t.Applicant_Id);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        IsPresent = c.Boolean(nullable: false),
                        CompanyName = c.String(),
                        Applicant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.Applicant_Id)
                .Index(t => t.Applicant_Id);
            
            CreateTable(
                "dbo.ApplicantReviewAssessments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InterviewQuestionId = c.Int(nullable: false),
                        ApplicationReviewId = c.Int(nullable: false),
                        Response = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationReviews", t => t.ApplicationReviewId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.InterviewQuestions", t => t.InterviewQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.InterviewQuestionId)
                .Index(t => t.ApplicationReviewId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ApplicationReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewerId = c.Int(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applications", t => t.ApplicationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .ForeignKey("dbo.Users", t => t.ReviewerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.ReviewerId)
                .Index(t => t.ApplicationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.InterviewQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(),
                        Question = c.String(),
                        IsGeneral = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.JobViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(),
                        Details = c.String(),
                        Title = c.String(),
                        DetailsURL = c.String(),
                        SentTo = c.String(),
                        Date = c.DateTime(),
                        IsRead = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        IsReminder = c.Boolean(),
                        Code = c.String(),
                        NotificationType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReviewResults",
                c => new
                    {
                        ReviewerId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        Appearance = c.Int(nullable: false),
                        Disposition = c.Int(nullable: false),
                        Communication = c.Int(nullable: false),
                        EducationalQualification = c.Int(nullable: false),
                        RelevantExperience = c.Int(nullable: false),
                        RelevantTechnicalExperience = c.Int(nullable: false),
                        AnalyticalReasoningAbility = c.Int(nullable: false),
                        GeneralKnowledge = c.Int(nullable: false),
                        EstimateOfIntelligence = c.Int(nullable: false),
                        GeneralRemark = c.String(),
                        Recommendation = c.String(),
                        TotalScore = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                    })
                .PrimaryKey(t => new { t.ReviewerId, t.ApplicantId })
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .ForeignKey("dbo.Users", t => t.ReviewerId, cascadeDelete: true)
                .Index(t => t.ReviewerId)
                .Index(t => t.ApplicantId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewResults", "ReviewerId", "dbo.Users");
            DropForeignKey("dbo.ReviewResults", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.ReviewResults", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ReviewResults", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantReviewAssessments", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.ApplicantReviewAssessments", "InterviewQuestionId", "dbo.InterviewQuestions");
            DropForeignKey("dbo.InterviewQuestions", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.InterviewQuestions", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.InterviewQuestions", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ApplicantReviewAssessments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ApplicationReviews", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ApplicantReviewAssessments", "ApplicationReviewId", "dbo.ApplicationReviews");
            DropForeignKey("dbo.ApplicationReviews", "ReviewerId", "dbo.Users");
            DropForeignKey("dbo.ApplicationReviews", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.ApplicationReviews", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ApplicationReviews", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.ApplicantDocuments", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.ApplicantDocuments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Experiences", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Applicants", "UserId", "dbo.Users");
            DropForeignKey("dbo.Skills", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Educations", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Applicants", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Institutions", "Applicant_Id", "dbo.Applicants");
            DropForeignKey("dbo.Applicants", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Applications", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Applications", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Jobs", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Applications", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Roles", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Roles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Users", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Departments", "HoDId", "dbo.Users");
            DropForeignKey("dbo.Departments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Applications", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ApplicantDocuments", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.ReviewResults", new[] { "LastModifiedById" });
            DropIndex("dbo.ReviewResults", new[] { "CreatedById" });
            DropIndex("dbo.ReviewResults", new[] { "ApplicantId" });
            DropIndex("dbo.ReviewResults", new[] { "ReviewerId" });
            DropIndex("dbo.InterviewQuestions", new[] { "LastModifiedById" });
            DropIndex("dbo.InterviewQuestions", new[] { "CreatedById" });
            DropIndex("dbo.InterviewQuestions", new[] { "DepartmentId" });
            DropIndex("dbo.ApplicationReviews", new[] { "User_Id" });
            DropIndex("dbo.ApplicationReviews", new[] { "LastModifiedById" });
            DropIndex("dbo.ApplicationReviews", new[] { "CreatedById" });
            DropIndex("dbo.ApplicationReviews", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationReviews", new[] { "ReviewerId" });
            DropIndex("dbo.ApplicantReviewAssessments", new[] { "LastModifiedById" });
            DropIndex("dbo.ApplicantReviewAssessments", new[] { "CreatedById" });
            DropIndex("dbo.ApplicantReviewAssessments", new[] { "ApplicationReviewId" });
            DropIndex("dbo.ApplicantReviewAssessments", new[] { "InterviewQuestionId" });
            DropIndex("dbo.Experiences", new[] { "Applicant_Id" });
            DropIndex("dbo.Skills", new[] { "Applicant_Id" });
            DropIndex("dbo.Educations", new[] { "Applicant_Id" });
            DropIndex("dbo.Institutions", new[] { "Applicant_Id" });
            DropIndex("dbo.Jobs", new[] { "LastModifiedById" });
            DropIndex("dbo.Jobs", new[] { "CreatedById" });
            DropIndex("dbo.Roles", new[] { "LastModifiedById" });
            DropIndex("dbo.Roles", new[] { "CreatedById" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "LastModifiedById" });
            DropIndex("dbo.UserRoles", new[] { "CreatedById" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Departments", new[] { "LastModifiedById" });
            DropIndex("dbo.Departments", new[] { "CreatedById" });
            DropIndex("dbo.Departments", new[] { "HoDId" });
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropIndex("dbo.Users", new[] { "LastModifiedById" });
            DropIndex("dbo.Users", new[] { "CreatedById" });
            DropIndex("dbo.Applications", new[] { "LastModifiedById" });
            DropIndex("dbo.Applications", new[] { "CreatedById" });
            DropIndex("dbo.Applications", new[] { "JobId" });
            DropIndex("dbo.Applications", new[] { "ApplicantId" });
            DropIndex("dbo.Applicants", new[] { "LastModifiedById" });
            DropIndex("dbo.Applicants", new[] { "CreatedById" });
            DropIndex("dbo.Applicants", new[] { "UserId" });
            DropIndex("dbo.ApplicantDocuments", new[] { "LastModifiedById" });
            DropIndex("dbo.ApplicantDocuments", new[] { "CreatedById" });
            DropIndex("dbo.ApplicantDocuments", new[] { "ApplicantId" });
            DropTable("dbo.ReviewResults");
            DropTable("dbo.Notifications");
            DropTable("dbo.JobViewModels");
            DropTable("dbo.InterviewQuestions");
            DropTable("dbo.ApplicationReviews");
            DropTable("dbo.ApplicantReviewAssessments");
            DropTable("dbo.Experiences");
            DropTable("dbo.Skills");
            DropTable("dbo.Educations");
            DropTable("dbo.Institutions");
            DropTable("dbo.Jobs");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Departments");
            DropTable("dbo.Users");
            DropTable("dbo.Applications");
            DropTable("dbo.Applicants");
            DropTable("dbo.ApplicantDocuments");
        }
    }
}
