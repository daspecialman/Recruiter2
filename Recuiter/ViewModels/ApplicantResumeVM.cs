﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
	public class ApplicantResumeVM
	{
		public  List<EducationVM> Education { get; set; }
		public List<Experience> Experience { get; set; }
		public List<Skill> Skill { get; set; }
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

	public class Experience
	{
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

		public string Company { get; set; }
	}



	public class Skill
	{
		public int Id { get; set; }

		public SkillLevel Skilllevel { get; set; }

		public string Achievement { get; set; }

	}
}