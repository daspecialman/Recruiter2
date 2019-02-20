using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Data.Models;
using Recruiter.Context;
using Recruiter.CustomAuthentication;
using Recruiter.ViewModels;

namespace Recruiter.Controllers
{
    public class JobsController : Controller
    {
        private RecruiterContext db = new RecruiterContext();
        
        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.CreatedBy)/*.Include(j => j.Department)*/.Include(j => j.LastModifiedBy)/*.Include(j => j.ContractClass)*/.Where(j => j.IsActive == false);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobViewModel jobCreateView)
        {
            if (ModelState.IsValid)
            {
                var user = Membership.GetUser(User.Identity.Name) as CustomMembershipUser;


                var jobs = new Job
                {
                    //Id = jobCreateView.JobId,
                    //DepartmentId = jobCreateView.DepartmentId,
                    Title = jobCreateView.Title,
                    //Summary = jobCreateView.Summary,
                    Description = jobCreateView.Description,
                    Responsibility = jobCreateView.Responsibility,
                    Characteristics = jobCreateView.Characteristics,
                    SkillSet = jobCreateView.SkillSet,
                    MinimumQualification = jobCreateView.MinimumQualification,
                    ExperienceLevel = jobCreateView.ExperienceLevel,
                    ContractClass = jobCreateView.ContractClass,
                    //ContractLength = jobCreateView.ContractLength,
                    ExpiryDate = jobCreateView.ExpiryDate,
                    CreatedDate = DateTime.Now
                };
                if (user != null)
                {
                    jobs.CreatedById = user.UserId;
                    jobs.LastModifiedById = user.UserId;
					
                }

                db.Jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", job.CreatedById);
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", job.DepartmentId);
            //ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", job.LastModifiedById);
            return View(jobCreateView);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.route = Request.CurrentExecutionFilePath;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            var jobVM = new JobViewModel(); //Need to change the name to a more generic name.
            if (job == null)
            {
                return HttpNotFound();
            }
            jobVM.Id = job.Id;
            //jobVM.JobId = job.JobId;
            //jobVM.DepartmentId = job.DepartmentId;
            jobVM.Title = job.Title;
            //jobVM.Summary = job.Summary;
            jobVM.Description = job.Description;
            jobVM.Responsibility = job.Responsibility;
            jobVM.Characteristics = job.Characteristics;
            jobVM.SkillSet = job.SkillSet;
            jobVM.MinimumQualification = job.MinimumQualification;
            jobVM.ExperienceLevel = job.ExperienceLevel;
            jobVM.ExperienceLength = job.ExperienceLength;
            jobVM.ContractClass = job.ContractClass;
            //jobVM.ContractLength = job.ContractLength;
            jobVM.ExpiryDate = job.ExpiryDate;
            jobVM.CreatedById = job.CreatedById;

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", job.CreatedById);
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", job.DepartmentId);

            return View(jobVM);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobViewModel jobVM)
        {
            var user = Membership.GetUser(User.Identity.Name) as CustomMembershipUser;
            //var job = new Job();
            var job = db.Jobs.Find(jobVM.Id);
            if (ModelState.IsValid)
            {
                //job.JobId = jobVM.JobId;
                //job.DepartmentId = jobVM.DepartmentId;
                job.Title = jobVM.Title;
                //job.Summary = jobVM.Summary;
                job.Description = jobVM.Description;
                job.Responsibility = jobVM.Responsibility;
                job.Characteristics = jobVM.Characteristics;
                job.SkillSet = jobVM.SkillSet;
                job.MinimumQualification = jobVM.MinimumQualification;
                job.ExperienceLevel = jobVM.ExperienceLevel;
                job.ExperienceLength = jobVM.ExperienceLength;
                job.ContractClass = jobVM.ContractClass;
                //job.ContractLength = jobVM.ContractLength;
                job.ExpiryDate = jobVM.ExpiryDate;
                job.LastModifiedById = user.UserId;
                job.LastModifiedDate = DateTime.Now;


                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", job.CreatedById);
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", job.DepartmentId);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", job.LastModifiedById);
            return View(jobVM);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            job.IsActive = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
