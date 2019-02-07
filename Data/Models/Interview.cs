using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class Interview
	{ 
		public DateTime StartTime { get; set; }
		
		public DateTime? EndTime { get; set; }

		public int DepartmentId { get; set; }

		public Department Department { get; set; }

		public QuestionTemplate QuestionTemplate { get; set; }

		public int QuestionTemplateId { get; set; }

		public ICollection<ApplicantInterview> ApplicantInterviews { get; set; }

		public ICollection<InterviewReviewer> InterviewReviewer { get; set; }
	}
}
