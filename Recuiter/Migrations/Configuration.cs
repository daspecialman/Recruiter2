namespace Recruiter.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using Data.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
    using Recruiter.Context;

    internal sealed class Configuration : DbMigrationsConfiguration<RecruiterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RecruiterContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.


			//if (!context.Roles.Any())
			//{
			//	string[] roleNames = { "Admin", "HOD", "Applicant" };
			//	foreach (var roleName in roleNames)
			//	{
			//		var role = new Role { Name = roleName , cr};
			//			context.Roles.Add(role);
			//	}
			//}

			//var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			//UserManager.AddToRole("UserId", "UserRole");
		}
    }
}
