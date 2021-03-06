﻿using System;
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
using Recruiter.App_Start.Filters;
using Recruiter.Context;
using Recruiter.CustomAuthentication;
using Recruiter.ViewModels;
using PagedList;
using Data.Enums;
using Recruiter.Utitlity;

namespace Recruiter.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class ApplicantsController : Controller
    {
        private RecruiterContext db;

        public ApplicantsController()
        {
            db = new RecruiterContext();
        }

        [HttpGet]
        public ActionResult Index(Search search)
        {

            var jobss = db.Jobs.Where(j => j.ExpiryDate > DateTime.Now);

            if(search != null)
            {
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
                

            }

            int pageNumber = (search.page ?? 1);

            var jobsss = jobss.OrderByDescending(s => s.CreatedDate).ToList();
            if (jobsss.Count() == 0)
            {
                ViewBag.Message = "No jobs found";
                return View();
            }

            return View(jobsss.ToPagedList(pageNumber, Constants.PageSize));
        }


     
        public ActionResult JobDetails(int? Id)
        {
            var jobDetails = (from job in db.Jobs
                              where job.Id == Id
                              select job).FirstOrDefault();

            var viewModel = new JobViewModel
            {
                Id = jobDetails.Id,
                Title = jobDetails.Title,
                ContractFormat = jobDetails.ContractClass.DescriptionAttr(),
                Responsibility = jobDetails.Responsibility,
                Characteristics = jobDetails.Characteristics,
                ExpiryDate = jobDetails.ExpiryDate,
                Description = jobDetails.Description,
                ExperienceLevel = jobDetails.ExperienceLevel,
                QualificationFormat = jobDetails.MinimumQualification.DescriptionAttr(),
                SkillSet = jobDetails.SkillSet
            };

            ViewBag.JobID = Id;
            ViewBag.TopAppliedJobs = GetTopAppliedJobs();
            return View(viewModel);
        }

        public ActionResult JobApplication(int? Id, int jobId)
        {
            if ((Id is null) || Id == 0)
            {
                var userId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;

                if (IsApplicant(userId))
                {
                    var applicantId = (db.Applicants.Where(a => a.UserId == userId).FirstOrDefault()).Id;
                    var now = DateTime.Now;

                    var aleadyApplied = db.Applications
                                        .Where(x => x.ApplicantId == applicantId)
                                        .Where(x => x.JobId == jobId)
                                        .FirstOrDefault();

                    if (aleadyApplied is null)
                    {
                        var application = new Application
                        {
                            ApplicantId = applicantId,
                            CreatedById = userId,
                            JobId = jobId,
                            CreatedDate = now,
                            Date = now,
                            Status = AppliedJobStatus.InProgress
                        };

                        db.Applications.Add(application);
                        db.SaveChanges();
                        ViewBag.JobApplicationSuccess = "You applied Successfully";
                        return View();
                    }
                    else
                    {
                        ViewBag.JobApplicationError = "You already applied for this Job";
                        return View();
                    }
                }
                ViewBag.JobApplicationError = "Error! Select a job to apply for. Thank you.";
                return View();
            } else
            {
                return RedirectToAction("index", "Home");
            }
        }

        public ActionResult Dashboard(int? applicantId)
        {
            if (applicantId == null)
            {
                var userId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
                applicantId = (db.Applicants.Where(a => a.UserId == userId).FirstOrDefault()).Id;
            }
            var dashboard = new DashboardVM();
            dashboard.ApplicantId = applicantId.Value;
            var applications = (from application in db.Applications where application.ApplicantId == applicantId select application).ToList();


            return View();
        }

        public ActionResult ApplicantProfileEditReadOnly()
        {
            var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
            using (RecruiterContext dbContext = new RecruiterContext())
            {
                ////var user = (from u in dbContext.Users where u.Id == currentUserId select u).FirstOrDefault();
                //var applicant = (from p in dbContext.Applicants.Include(x=> x.User)
                //				 .Include(x=> x.ApplicantDocuments)
                //				 where p.UserId == currentUserId
                //				 select new ApplicantProfileViewModels
                //				 {
                //					 Age = p.Age,
                //					 Bio = p.Bio,
                //					 EducationLevel = p.EducationLevel,
                //					 Certificates = p.ApplicantDocuments.Select(x => new ApplicantDocumentViewModel
                //					 {
                //						 FilePath = x.FilePath,
                //						 Name = x.Name,
                //						 Type = x.Type
                //					 }).ToList()
                //				 }).FirstOrDefault();

                //// learn how to join tables using linq;
                var query = (from p in dbContext.Applicants.Include(x => x.User)
                             where p.UserId == currentUserId
                             select new ApplicantProfileViewModels
                             {
                                 Age = p.Age,
                                 Bio = p.Bio,
                                 EducationLevel = p.EducationLevel,
                                 PhoneNumber = p.PhoneNumber,
                                 Email = p.User.Email,
                                 City = p.City,
                                 Country = p.Country,
                                 CompleteAddress = p.Address,
                                 YearsOfExperience = p.YearsOfExperience,
                                 FirstName = p.User.FirstName,
                                 LastName = p.User.LastName,
                                 Certificates = (from files in dbContext.ApplicantDocuments
                                                 where files.ApplicantId == p.Id && files.Type == FileType.Certificate && !files.IsActive
                                                 select new ApplicantDocumentViewModel
                                                 {
                                                     FilePath = files.FilePath,
                                                     Name = files.Name,
                                                     Type = files.Type
                                                 }).ToList()
                             }).FirstOrDefault();
                return View(query);
            }
        }
        [HttpGet]
        [Authorize]
        // GET: Applicants
        public ActionResult ApplicantProfileEdit(int? Id, int? jobId)
        {
            var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
            using (RecruiterContext dbContext = new RecruiterContext())
            {
                var query = (from p in dbContext.Applicants.Include(x => x.User)
                             where p.UserId == currentUserId
                             select new ApplicantProfileViewModels
                             {
                                 Id = p.Id,
                                 Age = p.Age,
                                 Bio = p.Bio,
                                 PhoneNumber = p.PhoneNumber,
                                 Email = p.User.Email,
                                 City = p.City,
                                 Country = p.Country,
                                 CompleteAddress = p.Address,
                                 YearsOfExperience = p.YearsOfExperience,
                                 ExperienceLevel = p.ExperienceLevel,
                                 EducationLevel = p.EducationLevel,
                                 FirstName = p.User.FirstName,
                                 LastName = p.User.LastName,
                                 Specialization = p.Specialization,
                                 JobTitle = p.JobTitle,
                                 Language = p.Languages,
                                 ImagePath = p.User.ImagePath,




                                 Certificates = (from files in dbContext.ApplicantDocuments
                                                 where files.ApplicantId == p.Id && files.Type == FileType.Certificate && !files.IsActive
                                                 select new ApplicantDocumentViewModel
                                                 {
                                                     FilePath = files.FilePath,
                                                     Name = files.Name,
                                                     Type = files.Type
                                                 }).ToList()
                             }).FirstOrDefault();
                ViewBag.jobId = jobId;
                return View(query);
            }
        }


        [HttpPost]
        [PreventUncompletedProfile]
        public ActionResult ApplicantProfileEdit(ApplicantProfileViewModels applicantProfileVM, string ImageUpload, string SaveAndContinue, string retURL, int? jobId)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
                using (RecruiterContext dbContext = new RecruiterContext())
                {
                    var applicant = dbContext.Applicants.Where(a => a.UserId == currentUserId).FirstOrDefault();
                    var user = dbContext.Users.Where(u => u.Id == currentUserId).FirstOrDefault();
                    if (applicant != null)
                    {
                        if (!(applicantProfileVM.ImageFile is null))
                        {
                            applicantProfileVM.ImagePath = UploadImage(applicantProfileVM.ImageFile);
                            if (string.IsNullOrEmpty(applicantProfileVM.ImagePath))
                            {
                                ModelState.AddModelError("", "Image upload failed");
                                return View(applicantProfileVM);
                            }
                            else
                            {
                                user.ImagePath = applicantProfileVM.ImagePath;
                                dbContext.Entry(user).State = EntityState.Modified;
                                ViewBag.Success = "Image Uploaded Successfully";
                                if (!string.IsNullOrEmpty(ImageUpload))
                                {
                                    dbContext.SaveChanges();
                                    return View(applicantProfileVM);
                                }

                            }
                        }

                        if (!string.IsNullOrEmpty(SaveAndContinue))
                        {

                            try
                            {
                                applicant.JobTitle = applicantProfileVM.JobTitle;
                                applicant.Specialization = applicantProfileVM.Specialization;
                                applicant.PhoneNumber = applicantProfileVM.PhoneNumber;
                                applicant.Address = applicantProfileVM.CompleteAddress;
                                applicant.Age = applicantProfileVM.Age;
                                applicant.Country = applicantProfileVM.Country;
                                applicant.City = applicantProfileVM.City;
                                applicant.Languages = applicantProfileVM.Language;
                                applicant.EducationLevel = (MinimumQualificationType)
                                applicantProfileVM.EducationLevel;
                                applicant.ExperienceLevel = (ExperienceLevelType)applicantProfileVM.ExperienceLevel;
                                applicant.Bio = applicantProfileVM.Bio;


                                dbContext.Entry(user).State = EntityState.Modified;
                                dbContext.SaveChanges();
                                ViewBag.Success = "Image Uploaded Successfully";

                                return RedirectToAction("ApplicantResumeProfile", new { id = applicantProfileVM.Id, jobId = jobId });

                            }
                            catch (Exception e)
                            {
                                ModelState.AddModelError("", e + ". Applicant upload failed");
                            }

                        }


                    }
                    //else if (applicant == null)
                    //{
                    //	var appnew = new Applicant()
                    //	{
                    //		PhoneNumber = applicant.PhoneNumber,
                    //		Address = applicant.Address,
                    //		Age = applicant.Age,
                    //		Country = applicant.Country,
                    //		City = applicant.City,
                    //		Bio = applicant.Bio,
                    //		EducationLevel = applicant.EducationLevel,
                    //		YearsOfExperience = applicant.YearsOfExperience,
                    //	};
                    //	dbContext.Applicants.Add(appnew);
                    //	dbContext.SaveChanges();
                    //}
                    else
                    {
                        ModelState.AddModelError("Warning Error", "Information is not correct");
                        return View(applicantProfileVM);
                    }
                }
            }
            return View(applicantProfileVM);
        }
        [HttpGet]
        public ActionResult ApplicantResumeProfile(int id, int? jobId)
        {
            var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;

            var returnObject = new ApplicantResumeVM();
            using (RecruiterContext dbContext = new RecruiterContext())
            {
                var applicantEntity = dbContext.Applicants
                                        .Where(a => a.UserId == currentUserId)
                                        .Include(x => x.User)
                                        .Include(x => x.PastEducation)
                                        .Include(x => x.Skills)
                                        .Include(x => x.ApplicantDocuments)
                                        .Include(x => x.Institutions).FirstOrDefault();
                returnObject = new ApplicantResumeVM
                {
                    Education = applicantEntity.PastEducation.Select(x =>
                    new Recruiter.ViewModels.EducationVM
                    {
                        Id = x.Id,
                        CourseStudies = x.CourseStudied,
                        FromDate = x.FromDate,
                        Institution = x.Institution,
                        Qualification = x.Qualification,
                        ToDate = x.ToDate
                    }).ToList(),
                    Experience = applicantEntity.WorkExperience.Select(x =>
                       new Recruiter.ViewModels.ExperienceVM
                       {
                           Id = x.Id,
                           Title = x.Title,
                           FromDate = x.FromDate,
                           Company = x.CompanyName,
                           ToDate = x.ToDate
                       }).ToList(),
                    Skill = applicantEntity.Skills.Select(x =>
                       new Recruiter.ViewModels.SkillVM
                       {
                           Id = x.Id,
                           SkillTitle = x.SkillTitle,
                           Skilllevel = x.Skilllevel,
                           Achievement = x.Achievement,
                       }).ToList()
                };

                ViewBag.jobId = jobId;
                return View(returnObject);
            }
        }


        [HttpPost]
        public ActionResult ApplicantResumeProfile(Applicant applicantProfileViewModel, string SubmitApplication, int? jobId)

        {
            ViewBag.jobId = jobId;
            //Temporary Fix
            if (!string.IsNullOrEmpty(SubmitApplication))
            {
                return RedirectToAction("JobApplication", new { id = applicantProfileViewModel.Id, jobId });
            }

            if (ModelState.IsValid)
            {

                var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
                using (RecruiterContext dbContext = new RecruiterContext())
                {
                    var applicantEntity = dbContext.Applicants
                                            .Where(a => a.UserId == currentUserId)
                                            .Include(x => x.User)
                                            .Include(x => x.PastEducation)
                                            .Include(x => x.Skills)
                                            .Include(x => x.WorkExperience)
                                            .Include(x => x.ApplicantDocuments)
                                            .Include(x => x.Institutions).FirstOrDefault();
                    if (applicantEntity == null)
                    //var   returnapp = new ApplicantResumeVM
                    {
                        var educationsFromDb = applicantEntity.PastEducation.ToList();
                        foreach (var vmEducation in applicantProfileViewModel.PastEducation)
                        {
                            var dbEdu = educationsFromDb.FirstOrDefault(x => x.Id == vmEducation.Id);
                            //is new
                            if (dbEdu == null)
                            {
                                dbEdu = new Education
                                {
                                    CourseStudied = vmEducation.CourseStudied,
                                    Qualification = vmEducation.Qualification,
                                    Institution = vmEducation.Institution,
                                    FromDate = vmEducation.FromDate,
                                    ToDate = vmEducation.ToDate
                                };
                                applicantEntity.PastEducation.Add(dbEdu);
                            }
                            //updating
                            else
                            {
                                dbEdu.Institution = vmEducation.Institution;
                                dbEdu.Qualification = vmEducation.Qualification;
                                dbEdu.ToDate = vmEducation.ToDate;
                                dbEdu.FromDate = vmEducation.FromDate;
                                dbEdu.CourseStudied = vmEducation.CourseStudied;
                                db.Entry(dbEdu).State = EntityState.Modified;
                            }
                        }
                        var WorkExperiencesFromDb = applicantEntity.WorkExperience.ToList();
                        foreach (var vmWorkExperience in applicantProfileViewModel.WorkExperience)
                        {
                            var dbExperience = WorkExperiencesFromDb.FirstOrDefault(x => x.Id == vmWorkExperience.Id);
                            //is new
                            if (dbExperience == null)
                            {
                                dbExperience = new Experience
                                {
                                    Title = vmWorkExperience.Title,
                                    FromDate = vmWorkExperience.FromDate,
                                    ToDate = vmWorkExperience.ToDate,
                                    CompanyName = vmWorkExperience.CompanyName
                                };
                                applicantEntity.WorkExperience.Add(dbExperience);
                            }
                            //updating
                            else
                            {
                                dbExperience.Title = vmWorkExperience.Title;
                                dbExperience.FromDate = vmWorkExperience.FromDate;
                                dbExperience.ToDate = vmWorkExperience.ToDate;
                                dbExperience.CompanyName = vmWorkExperience.CompanyName;
                                db.Entry(dbExperience).State = EntityState.Modified;
                            }
                        }
                        var SkillsFromDb = applicantEntity.Skills.ToList();
                        foreach (var vmSkill in applicantProfileViewModel.Skills)
                        {
                            var dbSkill = SkillsFromDb.FirstOrDefault(x => x.Id == vmSkill.Id);
                            //is new
                            if (dbSkill == null)
                            {
                                dbSkill = new Skill
                                {
                                    Achievement = vmSkill.Achievement,
                                    Id = vmSkill.Id,
                                    SkillTitle = vmSkill.SkillTitle,
                                    Skilllevel = vmSkill.Skilllevel
                                };
                                applicantEntity.Skills.Add(dbSkill);
                            }
                            //updating
                            else
                            {
                                dbSkill.Achievement = vmSkill.Achievement;
                                dbSkill.Id = vmSkill.Id;
                                dbSkill.SkillTitle = vmSkill.SkillTitle;
                                dbSkill.Skilllevel = vmSkill.Skilllevel;
                                db.Entry(dbSkill).State = EntityState.Modified;
                            }
                        }
                        dbContext.Applicants.Add(applicantEntity);
                        dbContext.SaveChanges();
                        return RedirectToAction("JobApplication", new { jobId = jobId });
                    }
                    else
                    {
                        ModelState.AddModelError("Warning Error", "Information is not correct");
                        return View(applicantProfileViewModel);
                    }
                }
            }
            return View(applicantProfileViewModel);
        }
        [HttpGet]
        public ActionResult ApplicantProfilePage()
        {
            var check = new ApplicantProfileViewModels();
            try
            {
                var loggedInUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
                using (RecruiterContext dbContext = new RecruiterContext())
                {

                    check = (from p in dbContext.Applicants.Include(x => x.User).Include(x => x.PastEducation).Include(x => x.Skills).Include(x => x.ApplicantDocuments)
                                    .Include(x => x.Institutions)
                             where p.UserId == loggedInUserId
                             //join image in dbContext.Images on new { Key1 = p.UserId, Key2 = false } equals new { Key1 = image.UserId, Key2 = image.IsDeleted } into P1
                             //from P2 in P1.DefaultIfEmpty()

                             select new ApplicantProfileViewModels
                             {
                                 Country = p.Country,
                                 City = p.City,
                                 CompleteAddress = p.Address,
                                 Achievement = p.Achievement,
                                 Age = p.Age,
                                 Bio = p.Bio,
                                 EducationLevel = p.EducationLevel,
                                 FirstName = p.User.FirstName,
                                 LastName = p.User.LastName,
                                 Language = p.Languages,
                                 PhoneNumber = p.PhoneNumber,
                                 ImagePath = p.User.ImagePath,
                                 
                                 PastEducation = dbContext.Educations.Where(x => x.ApplicantId == loggedInUserId).Select(x => new EducationVM()
                                 {
                                     CourseStudies = x.CourseStudied,
                                     FromDate = x.FromDate,
                                     Institution = x.Institution,
                                     Qualification = x.Qualification,
                                     ToDate = x.ToDate
                                 }).ToList(),
                                 WorkExperience = dbContext.Experiences.Where(x => x.ApplicantId == loggedInUserId).Select(x => new ExperienceVM
                                 {
                                     Company = x.CompanyName,
                                     FromDate = x.FromDate,
                                     Title = x.Title,
                                     Id = x.Id,
                                     ToDate = x.ToDate
                                 }).ToList(),
                                 Skills = dbContext.Skills.Where(x => x.ApplicantId == loggedInUserId).Select(x => new SkillVM
                                 {
                                    Id = x.Id,
                                    Achievement = x.Achievement,
                                    Skilllevel = x.Skilllevel,
                                    SkillTitle = x.SkillTitle
                                    
                                 }).ToList(),

                                 YearsOfExperience = p.YearsOfExperience,
                                 Certificates = (from files in dbContext.ApplicantDocuments
                                                 where files.ApplicantId == p.Id && files.Type == FileType.Certificate && !files.IsActive
                                                 select new ApplicantDocumentViewModel
                                                 {
                                                     FilePath = files.FilePath,
                                                     Name = files.Name,
                                                     Type = files.Type
                                                 }).ToList(),
                                 // Language = dbContext.Languages.Select(x => x.ApplicantId == p.Id, new Language { Name=})
                                 //Skills = (from details in dbContext.Applicants
                                 //	   where details.A )
                             }).FirstOrDefault();

                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(check);

        }


        public List<JobViewModel> GetTopAppliedJobs()
        {

            var sortedJobs = (from app in db.Applications
                              group app.JobId by app.JobId into appGroup
                              orderby appGroup.Count() descending
                              select new
                              {
                                  id = appGroup.Key,
                                  count = appGroup.Count()
                              }).ToList();
            var jobs = db.Jobs;
            var jobList = jobs.ToList();

            var topJobs = new List<JobViewModel>();
            foreach (var item in sortedJobs)
            {
                var formattedJob = jobList.Where(x => x.Id == item.id).FirstOrDefault();
                topJobs.Add(new JobViewModel
                {
                    Id = item.id,
                    Title = formattedJob.Title,
                    ExpiryDate = formattedJob.ExpiryDate,

                });
            }

            var count = topJobs.Count();

            foreach (var top in topJobs)
            {
                var rmv = jobs.Where(x => x.Id == top.Id).FirstOrDefault();
                jobList.Remove(rmv);
            }

            if (count < 5)
            {
                var addJobs = jobList.GetRange((5 - count), (5 - count)).Select(x => new JobViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ExpiryDate = x.ExpiryDate
                }).ToList();

                foreach (var addJob in addJobs)
                {
                    topJobs.Add(addJob);
                }
                return topJobs;
            } else {
                return topJobs.GetRange(0, 5);
            }
        }

        private bool IsApplicant (int userId)
        {
            var check = db.Applicants.Where(a => a.UserId == userId).FirstOrDefault();

            return (!(check is null)) ? true : false;
        }











































        // GET: Applicants/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }
        [HttpGet]
        // GET: Applicants/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }
        // POST: Applicants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Address,PhoneNumber,Country,City,YearsOfExperience,Age,EducationLevel,Bio,IsDeleted,CreatedDate,LastModifiedDate,CreatedById,LastModifiedById")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                db.Applicants.Add(applicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", applicant.CreatedById);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", applicant.LastModifiedById);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", applicant.UserId);
            return View(applicant);
        }
        // GET: Applicants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", applicant.CreatedById);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", applicant.LastModifiedById);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", applicant.UserId);
            return View(applicant);
        }
        // POST: Applicants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Address,PhoneNumber,Country,City,YearsOfExperience,Age,EducationLevel,Bio,IsDeleted,CreatedDate,LastModifiedDate,CreatedById,LastModifiedById")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "Username", applicant.CreatedById);
            ViewBag.LastModifiedById = new SelectList(db.Users, "Id", "Username", applicant.LastModifiedById);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", applicant.UserId);
            return View(applicant);
        }
        // GET: Applicants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }
        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            db.Applicants.Remove(applicant);
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



        [HttpPost]
        public ActionResult AddImage(UserVM imageModel)
        {

            try
            {
                string extension = Path.GetExtension(imageModel.file.FileName);

                var filename = "~/UploadedImages/" + DateTime.Now.ToString("yymmssfff") + extension;


                //	string fileName = Path.GetFileNameWithoutExtension(imageModel.file.FileName);
                //string extension = Path.GetExtension(imageModel.file.FileName);
                //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //imageModel.ImagePath = "~/UploadedImages/" + fileName;
                //fileName = Path.Combine(Server.MapPath("~/UploadedImages/"), fileName);

                imageModel.file.SaveAs(Server.MapPath(filename));

                var currentUser = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;

                using (var db = new RecruiterContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Id == currentUser);

                    if (user != null)
                    {
                        user.ImagePath = filename;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        var newUser = new User
                        {
                            ImagePath = imageModel.ImagePath,
                            IsActive = true
                        };
                        db.Users.Add(newUser);
                        db.SaveChanges();
                    }
                }

            }
            catch
            {

            }

            //db.Users.Add(imageModel);
            db.SaveChanges();

            TempData["user"] = imageModel;
            //ModelState.Clear();
            return RedirectToAction("ApplicantProfilePage");

        }

        public string UploadImage(HttpPostedFileBase ImageFile)
        {
            try
            {
                string extension = Path.GetExtension(ImageFile.FileName);
                var filename = "~/UploadedImages/" + DateTime.Now.ToString("yymmssfff") + extension;
                ImageFile.SaveAs(Server.MapPath(filename));
                return filename;
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Warning Error", "Information is not correct. " + e.Message);
                return "";
            }

            //return RedirectToAction("ApplicantProfilePage");

        }

        [HttpGet]
        public ActionResult AppliedJobs()
        {
            var customUser = Membership.GetUser(User.Identity.Name) as CustomMembershipUser;

            var applicant = db.Applicants
                                .Include(u => u.User)
                                .Where(x => x.UserId == customUser.UserId)
                                .FirstOrDefault();
            var jobList = db.Jobs.ToList();

            var applicationListVM = new List<ApplicationVM>();

            foreach (var application in applicant.Applications)
            {
                applicationListVM.Add(new ApplicationVM
                {
                    Id = application.Id,
                    JobTitle = (jobList.Where(x => x.Id == application.JobId).FirstOrDefault()).Title,
                    Date = application.Date,
                    Status = application.Status
                });
            }
            return View(applicationListVM);
        }





        //[HttpGet]
        [NonAction]
        public ActionResult AppliedJobs(int Id)
        {
            var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
            var returnObject = new ApplicationListVM();
            using (RecruiterContext dbContext = new RecruiterContext())
            {
                var applicantEntity = dbContext.Applicants
                                        .Where(a => a.UserId == currentUserId)
                                        .Include(x => x.User)
                                        .Include(x => x.Applications).FirstOrDefault();
                returnObject = new ApplicationListVM
                {
                    AppliedJobs = applicantEntity.Applications.Select(x =>
                    new ViewModels.ApplicationVM
                    {
                        JobTitle = x.JobTitle,
                        Date = x.Date,
                        Status = x.Status,
                    }).ToList(),
                };

            }

            return View(returnObject);
        }



        [HttpPost]
        public ActionResult AppliedJobs(Applicant applicationVM)
        {

            if (ModelState.IsValid)
            {
                var currentUserId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
                using (RecruiterContext dbContext = new RecruiterContext())
                {
                    var applicantEntity = dbContext.Applicants
                                            .Where(a => a.UserId == currentUserId)
                                            .Include(x => x.User)
                                            .Include(x => x.Applications).FirstOrDefault();

                    if (applicantEntity == null)
                    {
                        var jobsFromDb = applicantEntity.Applications.ToList();
                        foreach (var vmjob in applicationVM.Applications)
                        {
                            var dbjob = jobsFromDb.FirstOrDefault(x => x.Id == vmjob.Id);
                            //is new
                            if (dbjob == null)
                            {
                                dbjob = new Data.Models.Application
                                {
                                    JobTitle = vmjob.Job.Title,
                                    Status = vmjob.Status,
                                    Date = vmjob.LastModifiedDate,
                                };
                                applicantEntity.Applications.Add(dbjob);
                            }
                            //updating
                            else
                            {
                                dbjob.JobTitle = vmjob.JobTitle;
                                dbjob.Status = vmjob.Status;
                                dbjob.Date = vmjob.Date;

                            }
                        }
                        dbContext.Applicants.Add(applicantEntity);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("Warning Error", "Information is not correct");


                    }
                }

            }
            return View(applicationVM);
        }

    }
}








