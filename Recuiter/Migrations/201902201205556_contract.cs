namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contract : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobViewModels", "ContractFormat", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobViewModels", "ContractFormat");
        }
    }
}
