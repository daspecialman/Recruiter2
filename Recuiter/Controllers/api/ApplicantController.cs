using Recruiter.Context;
using Recruiter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Recruiter.Controllers.api
{
    [RoutePrefix("api/applicant")]
    public class ApplicantController : ApiController
    {
        private RecruiterContext db = new RecruiterContext();

        [HttpGet]
        [Route("Education/Id")]
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
                return NotFound();
            }

            return Json(result);



        }

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
                result.Message = "Successusfully Entered Experience";
                result.Data = experienceModel;
            }
            else
            {
                return NotFound();
            }
            return Json(result);
        }

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
                result.Message = "Successusfully Entered Skills";
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
