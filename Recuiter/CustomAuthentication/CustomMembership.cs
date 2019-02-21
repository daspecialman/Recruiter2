using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using Data.Models;
using Recruiter.Context;

namespace Recruiter.CustomAuthentication
{
	public class CustomMembership : MembershipProvider
	{


		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public override bool ValidateUser(string email, string password)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				return false;
			}

			using (RecruiterContext dbContext = new RecruiterContext())
			{
				/* user = (from us in dbContext.Users
							where string.Compare(email, us.Email, StringComparison.OrdinalIgnoreCase) == 1
							&& string.Compare(password, us.Password, StringComparison.OrdinalIgnoreCase) == 1
							// && us.IsActive == true
							select us).FirstOrDefault();*/

				var user = (from us in dbContext.Users
							where us.Email == email
							&& us.Password == password
							// && us.IsActive == true
							select us).FirstOrDefault();

				return (user != null) ? true : false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="email"></param>
		/// <param name="firstname"></param>
		/// <param name="lastname"></param>
		/// <param name="passwordQuestion"></param>
		/// <param name="passwordAnswer"></param>
		/// <param name="isApproved"></param>
		/// <param name="providerUserKey"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public override MembershipUser CreateUser(string username, string password, string email, string firstname, string lastname, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{

			using (RecruiterContext dbContext = new RecruiterContext())
			{
				var user = (from us in dbContext.Users
							where string.Compare(username, us.Username, StringComparison.OrdinalIgnoreCase) == 1
							select us).FirstOrDefault();

				if (user == null)
				{
					user = new User
					{
						Username = username,
						Password = password,
						Email = email,
						FirstName = firstname,
						LastName = lastname,
						IsActive = isApproved,
						CreatedDate = DateTime.Now,
					};

					dbContext.Users.Add(user);
					dbContext.SaveChanges();

					status = MembershipCreateStatus.Success;
					return new CustomMembershipUser(user) as MembershipUser;
				}
				else
				{
					status = MembershipCreateStatus.DuplicateUserName;
					return null;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <param name="userIsOnline"></param>
		/// <returns></returns>
		public override MembershipUser GetUser(string email, bool userIsOnline)
		{
			using (RecruiterContext dbContext = new RecruiterContext())
			{
				var user = (from us in dbContext.Users.Include(x => x.Roles)
							where string.Compare(email, us.Email, StringComparison.OrdinalIgnoreCase) == 0
							select us).FirstOrDefault();


				if (user != null)
				{
					List<UserRole> roles = (from r in dbContext.UserRoles.Include(x => x.Role)
											where r.UserId == user.Id
											select r).ToList();

					user.Roles = roles;
				}

				if (user == null)
				{
					return null;
				}

				var selectedUser = new CustomMembershipUser(user);

				return selectedUser;
			}
		}


		public override string GetUserNameByEmail(string email)
		{
			using (RecruiterContext dbContext = new RecruiterContext())
			{
				string username = (from u in dbContext.Users
								   where string.Compare(email, u.Email) == 0
								   select u.Username).FirstOrDefault();

				return !string.IsNullOrEmpty(username) ? username : string.Empty;
			}
		}

		#region Overrides of Membership Provider

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

		public override bool EnablePasswordReset
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool EnablePasswordRetrieval
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MinRequiredPasswordLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int PasswordAttemptWindow
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override string PasswordStrengthRegularExpression
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool RequiresUniqueEmail
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{

			throw new NotImplementedException();
		}

	public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}