using Recruiter.Context;
using Recruiter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Security;
using Recruiter.CustomAuthentication;

namespace Recruiter.Controllers.api
{
    [RoutePrefix("api/applicant")]
    public class ApplicantApiController : ApiController
    {
        private RecruiterContext db = new RecruiterContext();


        [HttpPost]
        [Route("AddorUpdate")]
        public IHttpActionResult GetEducation(EducationVM model)
        {
            var response = new ApiResult<EducationVM>();
            var currentApplicantId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).ApplicantId;

            if (currentApplicantId == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Incorrect data posted, please check form and try again");

            if (model.Id > 0)
            {
                var education = db.Educations.Where(x => x.Id == model.Id).FirstOrDefault();
                if (education == null)
                    return NotFound();
                else
                {

                    education.Institution = model.Institution;
                    education.CourseStudied = model.CourseStudies;
                    education.FromDate = model.FromDate;
                    education.ToDate = model.ToDate;
                    education.Qualification = model.Qualification;
                    db.SaveChanges();
                }
                response.Message = "Updated successfully";
            }
            else
            {
                var education = new Education
                {
                    ApplicantId = (int)currentApplicantId,
                    Institution = model.Institution,
                    CourseStudied = model.CourseStudies,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    Qualification = model.Qualification
                };
               
                db.Educations.Add(education);
                model.Id = education.Id;
                response.Message = "Added successfully";
                
            }
            response.Data = model;
            response.HasError = false;
            return Ok(response);
        }


        [Route("Get")]
        public IHttpActionResult Get() {
            return Ok("Successful");
        }

        [HttpGet]
        [Route("Education/{Id}")]
        public IHttpActionResult GetEducation(int Id)
        {
            var education = db.Educations.Where(x => x.Id == Id).FirstOrDefault();
            var result = new ApiResult<EducationVM>();
            if (education != null)
            {
                var educationModel = new EducationVM
                {

                };

                result.HasError = false;
                result.Message = "great";
                result.Data = educationModel;
            }
            else
            {
               // NotFound()
                return BadRequest("Eductation records not found");
            }

            return Json(result);



        }


        [HttpGet]
        [Route("Experience/Id")]
        public IHttpActionResult GetExperience(int Id)
        {
            var experience = db.Experiences.Where(x => x.Id == Id).FirstOrDefault();
            var result = new ApiResult<ExperienceVM>();
            if (experience != null)
            {
                var experienceModel = new ExperienceVM
                {

                };

                result.HasError = false;
                result.Message = "Successusfully entered Experience";
                result.Data = experienceModel;
            }
            else
            {
                return NotFound();
            }
            return Json(result);
        }


        [HttpGet]
        [Route("Skill/Id")]
        public IHttpActionResult GetSkill(int Id)
        {
            var skill = db.Skills.Where(x => x.Id == Id).FirstOrDefault();
            var result = new ApiResult<SkillVM>();
            if (skill != null)
            {
                var skillModel = new SkillVM
                {

                };

                result.HasError = false;
                result.Message = "Successusfully entered Skills";
                result.Data = skillModel;
            }
            else
            {
                return NotFound();

            }
            return Json(result);
        }
    }
}
