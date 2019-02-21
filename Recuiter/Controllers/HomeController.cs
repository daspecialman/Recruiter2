using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Data.Models;
using Recruiter.Context;
using PagedList;
using Recruiter.Utitlity;


namespace Recuiter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        //RecruiterContext ctxt = new RecruiterContext();
        //ctxt.Roles.ToList();

        //return View();
        private RecruiterContext db;

        public HomeController()
        {
            db = new RecruiterContext();

        }


        //private readonly object applicationProfileViewModel;

        [HttpGet]
        public ActionResult Index(Search search, int? page)
          {

            TempData["State"] = search;
            DateTime curDate = DateTime.Now;
            //var jobss = from j in db.Jobs
            //            where j.ExpiryDate > curDate.Date
            //            select j;

            var jobss = db.Jobs.Where(j => j.ExpiryDate > curDate.Date);

            //SqlFunctions.DatePart("Year", j.ExpiryDate) == curDate.Year
            //        && SqlFunctions.DatePart("Month", j.ExpiryDate) == curDate.Month
            //        && SqlFunctions.DatePart("Day", j.ExpiryDate) == curDate.Day

            /*.Include(x => x.Department)*/


            if (!String.IsNullOrEmpty(search.SkillSearch))
            {
                jobss = jobss.Where(s => s.SkillSet.ToString().Contains(search.SkillSearch));
            }

            //Job Type Search
            if (search.JobType != null)
            {
                jobss = jobss.Where(x => search.JobType.Contains(x.ContractClass));
            }


            //Experience Based Search
            if (search.Experience != null)
            {
                jobss = jobss.Where(x => search.Experience.Contains(x.ExperienceLevel));
            }


            //For page numbers

            int pageNumber = (page ?? 1);

            var jobsss = jobss.OrderByDescending(s => s.CreatedDate).ToList();
            if (jobsss.Count() == 0)
            {
                ViewBag.Message = "No jobs found";
            }
            return View(jobsss.ToPagedList(pageNumber, Constants.PageSize));
            //return View(jobsss);
        }




    }
}