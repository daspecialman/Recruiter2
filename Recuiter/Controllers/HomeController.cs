using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Data.Models;
using Recruiter.Context;
using Recruiter.CustomAuthentication;
using Recruiter.ViewModels;
namespace Recuiter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        //RecruiterContext ctxt = new RecruiterContext();
        //ctxt.Roles.ToList();

        //return View();
        private RecruiterContext db = new RecruiterContext();
        //private readonly object applicationProfileViewModel;

        [HttpGet]
            public ActionResult Index(string searchString, string searchSkills, string searchContract, int? ContractClass, int? ExperienceLevel)
            {
                var jobss = from j in db.Jobs/*.Include(x => x.Department)*/ select j;

                if (!String.IsNullOrEmpty(searchString))
                {
                    jobss = jobss.Where(s => s.Title.Contains(searchString));
                }

                //if (!String.IsNullOrEmpty(searchSkills))
                //{
                //    jobss = jobss.Where(s => s.SkillSet.Contains(searchSkills));
                //}

                if (ContractClass != null)
                {
                    jobss = jobss.Where(x => x.ContractClass == (ContractClassType)ContractClass);


                    ViewBag.SearchFilter = new Search { Contract = ContractClass };

                }

                if (ExperienceLevel != null)
                {
                    jobss = jobss.Where(x => x.ExperienceLevel == (ExperienceLevelType)ExperienceLevel);

                    ViewBag.SearchFilter = new Search { Expereince = ExperienceLevel };
                }

                //var jobList = new List<JobViewModel>();
                //foreach(Job job in jobsss)
                //{

                //	var jobView = new JobViewModel
                //	{
                //		Id = job.Id,
                //		JobId = job.JobId,
                //		DepartmentId = job.DepartmentId,
                //		Title = job.Title,
                //		Summary = job.Summary,
                //		Description = job.Description,
                //		Responsibility = job.Responsibility,
                //		GeneralRequirement = job.GeneralRequirement,
                //		SkillSet = job.SkillSet,
                //		MinimumQualification = job.MinimumQualification,
                //		ExperienceLevel = job.ExperienceLevel,
                //		ExperienceLength = job.ExperienceLength,
                //		ContractClass = job.ContractClass,
                //		ExpiryDate = job.ExpiryDate
                //	};
                //jobList.Add(jobView);
                //}
                var jobsss = jobss.ToList();

                return View(jobsss);
            }



        
    }
}