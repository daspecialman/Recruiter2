using Data.Models;
using Recuiter.CustomAuthentication;
using Recuiter.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using Recruiter.Context;
using System.Configuration.Provider;

namespace Recruiter.CustomAuthentication
{
    public class CustomRole : RoleProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userRoles = new string[] { };

            RecruiterContext dbContext = new RecruiterContext();

            /*var selectedUser = (from us in dbContext.Users.Include("Roles")
                                where string.Compare(us.Username, username, StringComparison.OrdinalIgnoreCase) == 0
                                select us).FirstOrDefault();*/

           
            var selectedUser = dbContext.UserRoles.Where(u => u.User.Username == username);
            if (selectedUser != null)
            {
               
                var roles = selectedUser.Select(c => c.Role.Name).ToArray();
                return roles;
            }
            return null;


        }



        #region Overrides of Role Provider

        public override string ApplicationName
        {
            get
            {
            throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

		public override void AddUsersToRoles(string[] usernames, string[] rolenames)
		{
			throw new NotImplementedException();

		}

		public bool AddUserToRole(UserRole userRole)
		{
			if (userRole != null)
			{
				RecruiterContext db = new RecruiterContext();
				db.UserRoles.Add(userRole);
				var ret = db.SaveChanges();
				return (db.SaveChanges() == 1) ? true : false;
			}
			else
			{
				return false;
			}
		}

		public bool CreateRole(Role role) {
			using (RecruiterContext dbContext = new RecruiterContext())
			{
				var roles = (from us in dbContext.Roles
							 where string.Compare(role.Name, us.Name, StringComparison.InvariantCultureIgnoreCase) == 0
							 select us).FirstOrDefault();
				if (roles == null)
				{
					dbContext.Roles.Add(role);
					dbContext.SaveChanges();
					return true;
				}
				else {
					return false;
				}
			}
		}


        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
			var db = new RecruiterContext();
			var roles = db.Roles.ToList();
			string[] roleArray = new string[roles.Count];
			int n = 0;
			foreach (Role role in roles)
			{
				roleArray[n] = role.Name.ToString();
				n++;
			}
			return roleArray;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}