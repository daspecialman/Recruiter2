using Data.Models;
using Recruiter.Context;
using Recruiter.CustomAuthentication;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Recruiter.App_Start.Filters
{
	public class PreventUncompletedProfileAttribute : Attribute, IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{

		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null || filterContext.HttpContext == null)
			{
				return;
			}

			var user = filterContext.HttpContext.User;

			if (user.Identity.IsAuthenticated && user.IsInRole("Applicant"))
			{
				var currentUserId = (Membership.GetUser(user.Identity.Name) as CustomMembershipUser).UserId;
				using (RecruiterContext dbContext = new RecruiterContext())
				{
					var applicant = dbContext.Applicants.Where(a => a.ApplicantId == currentUserId).FirstOrDefault();
					if (applicant == null && !applicant.IsValid())
					{
						filterContext.Result = new RedirectResult("/applicant/applicantprofileedit");
					}
				}
			}
		}
	}
}