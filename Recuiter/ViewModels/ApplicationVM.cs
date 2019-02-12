using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recruiter.ViewModels
{
    public class ApplicationVM
    {
        public List<Application>AppliedJobs{ get; set; }
    }

	public class Application
	{
		[Display(Name = "Job Title")]
		public string JobTitle { get; set; }

		[Display(Name = "Date Applied")]
		public DateTime? Date { get; set; }


		[Display(Name = "Job Status")]
		public JobApplicationWorkFlow Status { get; set; }
	}
}