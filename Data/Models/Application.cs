
using Data.Enums;
using System;

namespace Data.Models
{
    public class Application : BaseModel
	{ 
        public int ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

		public string JobTitle { get; set; }

		public Job Job { get; set; }

		public DateTime? Date { get; set; }

		public int JobId { get; set; }

		public JobApplicationWorkFlow Status { get; set; }
	}
}