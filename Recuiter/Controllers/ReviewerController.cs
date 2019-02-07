using Data.Models;
using Recruiter.Context;
using Recruiter.CustomAuthentication;
using Recruiter.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Recruiter.Controllers
{
    public class ReviewerController : Controller
    {
        private RecruiterContext db;

        public ReviewerController()
        {
            db = new RecruiterContext();
        }
        // GET: Reviewer
        public ActionResult Index()
        {
            return View(db.Applications.ToList());
        }

        [ActionName("ReviewQuestions")]
        public ActionResult GetQuestions(int? Id)
        {
            TempData["ApplicantId"] = Id;
            return View();
        }

        [ActionName("Review")]
        [HttpPost]
        public ActionResult SubmitReview( AssessmentVM assessment )
        {
            int totalScore = assessment.Appearance
                    + assessment.Disposition
                    + assessment.Communication
                    + assessment.EducationalQualification
                    + assessment.RelevantExperience
                    + assessment.RelevantTechnicalExperience
                    + assessment.AnalyticalReasoningAbility
                    + assessment.GeneralKnowledge
                    + assessment.EstimateOfIntelligence;

            //var ass = (ICollection<AssessmentVM>)assessment;
            var reviewerId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId;
            var result = new ReviewResult
            {
                //ReviewerId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).UserId,
                //ApplicantId = assessment.ApplicantId,
                ReviewerId = reviewerId,
                CreatedById = reviewerId,
                ApplicantId = (db.Applicants.Where(a => a.UserId == assessment.ApplicantId).FirstOrDefault()).Id,
                Appearance = assessment.Appearance,
                Disposition = assessment.Disposition,
                Communication = assessment.Communication,
                EducationalQualification = assessment.EducationalQualification,
                RelevantExperience = assessment.RelevantExperience,
                RelevantTechnicalExperience = assessment.RelevantTechnicalExperience,
                AnalyticalReasoningAbility = assessment.AnalyticalReasoningAbility,
                GeneralKnowledge = assessment.GeneralKnowledge,
                EstimateOfIntelligence = assessment.EstimateOfIntelligence,
                GeneralRemark = assessment.GeneralRemark,
                Recommendation = assessment.Recommendation,
                TotalScore = totalScore
            };

            db.ReviewResults.Add(result);
            db.SaveChanges();
            return View();
        }
    }
}