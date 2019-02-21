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
        public IHttpActionResult AddEducation(EducationVM model)
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
                db.SaveChanges();
                model.Id = education.Id;

                response.Message = "Added successfully";

            }


            response.Data = model;
            response.HasError = false;
            return Ok(response);
        }

        [HttpDelete]
        [Route("Education/{Id}")]
        public IHttpActionResult DeleteEducation(int Id)
        {
            var education = db.Educations.Where(x => x.Id == Id).FirstOrDefault();
            if (education == null)
                return NotFound();
            else
            {
                db.Educations.Remove(education);
                db.SaveChanges();
                return Ok("Deleted");
            }

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
                    Institution = education.Institution,
                    Qualification = education.Qualification,
                    CourseStudies = education.CourseStudied,
                    Id = education.Id
                };

                result.HasError = false;
                result.Message = "Successfully entered Education";
                result.Data = educationModel;
            }
            else
            {
                // NotFound()
                return BadRequest("Eductation records not found");
            }

            return Json(result);



        }

        ////----------------------EXPERIENCE API----------------------------//////
        [HttpPost]
        [Route("AddExperience")]
        public IHttpActionResult AddExperience(ExperienceVM model)
        {
            var response = new ApiResult<ExperienceVM>();
            var currentApplicantId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).ApplicantId;

            if (currentApplicantId == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Incorrect data posted, please check form and try again");

            if (model.Id > 0)
            {
                var experience = db.Experiences.Where(x => x.Id == model.Id).FirstOrDefault();
                if (experience == null)
                    return NotFound();
                else
                {
                    experience.Title = model.Title;
                    experience.CompanyName = model.Company;
                    experience.FromDate = model.FromDate;
                    experience.ToDate = model.ToDate;
                    //education.Qualification = model.Qualification;
                    db.SaveChanges();
                }
                response.Message = "Updated successfully";
            }
            else
            {
                var experience = new Experience
                {
                    ApplicantId = (int)currentApplicantId,
                    Title = model.Title,
                    CompanyName = model.Company,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    //Qualification = model.Qualification
                };

                db.Experiences.Add(experience);
                db.SaveChanges();
                model.Id = experience.Id;

                response.Message = "Added successfully";

            }


            response.Data = model;
            response.HasError = false;
            return Ok(response);
        }

        [HttpDelete]
        [Route("Experience/{Id}")]
        public IHttpActionResult DeleteExperience(int Id)
        {
            var experience = db.Experiences.Where(x => x.Id == Id).FirstOrDefault();
            if (experience == null)
                return NotFound();
            else
            {
                db.Experiences.Remove(experience);
                db.SaveChanges();
                return Ok("Deleted");
            }

        }

        [HttpGet]
        [Route("Experience/{Id}")]
        public IHttpActionResult GetExperience(int Id)
        {
            var experience = db.Experiences.Where(x => x.Id == Id).FirstOrDefault();
            var result = new ApiResult<ExperienceVM>();
            if (experience != null)
            {
                var experienceModel = new ExperienceVM
                {
                    Title = experience.Title,
                    Company = experience.CompanyName,
                    Id = experience.Id

                };

                result.HasError = false;
                result.Message = "Successusfully entered Experience";
                result.Data = experienceModel;
            }
            else
            {
                return BadRequest("Experience records not found");
            }
            return Json(result);
        }

        
        /// -------------------------SKILLS API -----------------------------------------/////////
        [HttpPost]
        [Route("AddSkills")]
        public IHttpActionResult AddSkills(SkillVM model)
        {
            var response = new ApiResult<SkillVM>();
            var currentApplicantId = (Membership.GetUser(User.Identity.Name) as CustomMembershipUser).ApplicantId;


            if (currentApplicantId == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Incorrect data posted, please check form and try again");

            if (model.Id > 0)
            {
                var skill = db.Skills.Where(x => x.Id == model.Id).FirstOrDefault();
                if (skill == null)
                    return NotFound();
                else
                {

                    skill.SkillTitle = model.SkillTitle;
                    skill.Skilllevel = model.Skilllevel;
                    //skill.FromDate = model.FromDate;
                    //skill.ToDate = model.ToDate;
                    //skill.Qualification = model.Qualification;
                    db.SaveChanges();
                }
                response.Message = "Updated successfully";
            }
            else
            {
                var skill = new Skill
                {
                    ApplicantId = (int)currentApplicantId,
                    SkillTitle = model.SkillTitle,
                    Skilllevel = model.Skilllevel,
                    //FromDate = model.FromDate,
                    //ToDate = model.ToDate,
                    //Qualification = model.Qualification
                };

                db.Skills.Add(skill);
                db.SaveChanges();
                model.Id = skill.Id;

                response.Message = "Added successfully";

            }


            response.Data = model;
            response.HasError = false;
            return Ok(response);
        }

        [HttpDelete]
        [Route("Skills/{Id}")]
        public IHttpActionResult DeleteSkill(int Id)
        {
            var skill = db.Skills.Where(x => x.Id == Id).FirstOrDefault();
            if (skill == null)
                return NotFound();
            else
            {
                db.Skills.Remove(skill);
                db.SaveChanges();
                return Ok("Deleted");
            }

        }


        [HttpGet]
        [Route("Skills/{Id}")]
        public IHttpActionResult GetSkill(int Id)
        {
            var skill = db.Skills.Where(x => x.Id == Id).FirstOrDefault();
            var result = new ApiResult<SkillVM>();
            if (skill != null)
            {
                var skillModel = new SkillVM
                {
                    Id = skill.Id,
                    SkillTitle = skill.SkillTitle,
                    Skilllevel = skill.Skilllevel
                   
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
