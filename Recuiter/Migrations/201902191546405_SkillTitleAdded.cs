namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillTitleAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "SkillTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "SkillTitle");
        }
    }
}
