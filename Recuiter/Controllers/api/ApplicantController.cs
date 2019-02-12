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
    }
}
