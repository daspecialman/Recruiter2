namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newEmailCredentials : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Jobs", name: "Department_Id", newName: "DepartmentId");
            RenameIndex(table: "dbo.Jobs", name: "IX_Department_Id", newName: "IX_DepartmentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Jobs", name: "IX_DepartmentId", newName: "IX_Department_Id");
            RenameColumn(table: "dbo.Jobs", name: "DepartmentId", newName: "Department_Id");
        }
    }
}
