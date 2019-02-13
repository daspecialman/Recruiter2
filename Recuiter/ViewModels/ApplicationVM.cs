using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recruiter.ViewModels
{
    public class ApplicationListVM
    {
        public List<ApplicationVM>AppliedJobs{ get; set; }
    }

	public class ApplicationVM
	{
        public int Id { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

		public DateTime? Date { get; set; }

		public AppliedJobStatus Status { get; set; }
	}
}