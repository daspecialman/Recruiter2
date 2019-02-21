using Data.Models;
using Recruiter.Context;
using Recruiter.CustomAuthentication;
using Recruiter.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Recruiter.Controllers
{
	public class HrController : Controller
	{
		// GET: HrController
		public ActionResult Index()
		{
			return View();
		}


		[HttpGet]
		public ActionResult TotalApplicantApplied()
		{
			//var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
			var applicant = Membership.GetUser();
			using (RecruiterContext db = new RecruiterContext())
			{
				var appliedapplicant = (from p in db.Applicants.Include(x => x.User).Include(x => x.Applications)
										.Include(x => x.Job)
										select new
										{
											FirstName = p.User.FirstName,
											LastName = p.User.LastName,
											Email = p.User.Email,
											PhoneNumber = p.PhoneNumber,
											DateApplied = p.User.LastModifiedDate,
											JobTitle = p.Job.Title,
											Department = p.Job.Department.Name,
										}).ToList()
										.Select(c => new TotalApplicantAppliedVM
										{
											FirstName = c.FirstName,
											LastName = c.LastName,
											Email = c.Email,
											PhoneNumber = c.PhoneNumber,
											JobTitle = c.JobTitle,
											DateApplied = c.DateApplied,
											Department = c.Department,
										}).ToList();


				return View(appliedapplicant);

			}
		}



		public ActionResult PostedJobs()
		{
			using (RecruiterContext dbContext = new RecruiterContext())
			{
				var postedJobs = (from p in dbContext.Jobs
								  select new
								  {
									  Department = p.Department.Name,
									  ContractClass = p.ContractClass,
									  //Status = p.
									  DatePosted = p.CreatedDate,
								  }).ToList()
								  .Select(j => new PostedJobVM
								  {
									  Department = j.Department,
									  ContractClass = j.ContractClass,
									  //Status = j.Status,
									  DatePosted = j.DatePosted,
								  }).ToList();

				return View(postedJobs);
			}

		}


		public ActionResult MostResentApplications()
		{
			var applicant = Membership.GetUser();
			using (RecruiterContext db = new RecruiterContext())
			{
				var appliedapplicant = (from p in db.Applicants.Include(x => x.User).Include(x => x.Applications)
										.Include(x => x.Job)
										select new
										{
											DateApplied = p.User.LastModifiedDate,
											JobTitle = p.Job.Title,
											Department = p.Job.Department.Name,
										}).ToList()
										.Select(c => new TotalApplicantAppliedVM
										{
											JobTitle = c.JobTitle,
											DateApplied = c.DateApplied,
											Department = c.Department,
										}).OrderByDescending(o => o.JobTitle).Take(10).ToList();


				return View(appliedapplicant);



			}

		}
	}
}