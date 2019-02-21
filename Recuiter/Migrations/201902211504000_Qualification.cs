namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Qualification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Educations", "Qualification", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Educations", "Qualification", c => c.String());
        }
    }
}
