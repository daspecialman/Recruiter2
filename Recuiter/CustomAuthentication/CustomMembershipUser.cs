using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Data.Models;

namespace Recruiter.CustomAuthentication
{
    public class CustomMembershipUser : MembershipUser
    {
		private Applicant applicant;
		#region User Properties

		public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRole> Roles { get; set; }

        #endregion

        public CustomMembershipUser(User user) : base("CustomMembership", user.Username, user.Id, user.Username, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Roles = user.Roles;
        }

		public CustomMembershipUser(Applicant applicant)
		{
			this.applicant = applicant;
		}

		internal object ChangePassword(string v1, object oldPassword, string v2, object newPassword)
        {
            throw new NotImplementedException();
        }
    }
    
}