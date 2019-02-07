using Data.Models;
using Recruiter.CustomAuthentication;
using System;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Recruiter.Context
{
	public class DbInsertOnAppStart
	{
		public void Seed()
		{
			var username = "Admin Ebun";
			var password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
							.ComputeHash(Encoding.UTF8.GetBytes("password")));
			var email = "ebunsbsc@gmail.com";
			var firstname  = "Ebunoluwa";
			var lastname = "Abiona";

			var membership = new CustomMembership();

			var adminUser = membership.GetUser(email, false);
			if (adminUser == null)
			{
				adminUser = membership.CreateUser(username,  password, email, firstname, lastname, true, null, out MembershipCreateStatus status);

				switch (status)
				{
					case MembershipCreateStatus.Success:

						break;
					case MembershipCreateStatus.InvalidUserName:
						break;
					case MembershipCreateStatus.InvalidPassword:
						break;
					case MembershipCreateStatus.InvalidQuestion:
						break;
					case MembershipCreateStatus.InvalidAnswer:
						break;
					case MembershipCreateStatus.InvalidEmail:
						break;
					case MembershipCreateStatus.DuplicateUserName:
						break;
					case MembershipCreateStatus.DuplicateEmail:
						break;
					case MembershipCreateStatus.UserRejected:
						break;
					case MembershipCreateStatus.InvalidProviderUserKey:
						break;
					case MembershipCreateStatus.DuplicateProviderUserKey:
						break;
					case MembershipCreateStatus.ProviderError:
						break;
					default:
						break;
				}
			}


			var roleProvider = new CustomRole();

			if (roleProvider.GetAllRoles().Length <= 0)
			{
				var roles = new string[] { "Admin","Applicant" };
				var createdById = (adminUser as CustomMembershipUser).UserId;

				foreach (string roleName in roles)
				{
					var role = new Role
					{
						Name = roleName,
						CreatedById = createdById,
						CreatedDate = DateTime.Now
					};

					roleProvider.CreateRole(role);
				}

			using (RecruiterContext db = new RecruiterContext())
			{
				var userRole = new UserRole
				{
					RoleId = (db.Roles.Where(r => r.Name == "Admin").FirstOrDefault()).Id,
					UserId = createdById,
					CreatedById = createdById,
					LastModifiedById = createdById
				};

					roleProvider.AddUserToRole(userRole);
			}

			}
		}
	}
}