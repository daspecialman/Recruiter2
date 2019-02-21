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
        private RecruiterContext db = new RecruiterContext();



        //private readonly object applicationProfileViewModel;

        [HttpGet]
        public ActionResult Index(Search search, int? page)
        {


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
                foreach (var type in search.JobType)
                {
                    var filtered = (type != 0) ? 
                                        jobss.Where(x => x.ContractClass == (ContractClassType)type)
                                        : null;
                    jobss = (filtered != null) ? filtered : jobss;
                }
            }


            //Experience Based Search
            if (search.Experience != null)
            {
                foreach (var experience in search.Experience)
                {
                    var filtered = (experience != 0) ?
                                        jobss.Where(x => x.ExperienceLevel == (ExperienceLevelType)experience)
                                        : null;
                    jobss = (filtered != null) ? filtered : jobss;
                }
            }


            //For page numbers

            int pageNumber = (page ?? 1);

            var jobsss = jobss.ToList();
            return View(jobsss.ToPagedList(pageNumber, Constants.PageSize));
        }




    }
}