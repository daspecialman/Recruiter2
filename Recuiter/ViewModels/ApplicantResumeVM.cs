using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
	public class ApplicantResumeVM
	{   public int Id { get; set; }
		public  List<EducationVM> Education { get; set; }
		public List<ExperienceVM> Experience { get; set; }
		public List<SkillVM> Skill { get; set; }
	}

	public class EducationVM
	{
		public string Qualification { get; set; }

        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

		public string Institution { get; set; }

        public bool IsPresent { get; set; }

		public string CourseStudies { get; set; }

        public int Id { get; set; }
    }

	public class ExperienceVM
	{
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

		public string Company { get; set; }
	}



	public class SkillVM
	{
		public int Id { get; set; }

		public SkillLevel Skilllevel { get; set; }

		public string Achievement { get; set; }

	}
}