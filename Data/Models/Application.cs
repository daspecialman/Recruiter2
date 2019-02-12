
using Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Application : BaseModel
	{ 
        public int ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

		public Job Job { get; set; }

        [Display(Name = "Date Applied")]
        public DateTime? Date { get; set; }

		public int JobId { get; set; }

        [Display(Name = "Job Status")]
        public AppliedJobStatus Status { get; set; }
	}
}