using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class ApplicationInterviewReview
	{

		public int InterviewReviewerId { get; set; }

		public InterviewReviewer InterviewReviewer { get; set; }

		public int ReviewerId { get; set; }

		public User Reviewer { get; set; }

		public int ApplicantInterviewId { get; set; }

		public ApplicantInterview ApplicantInterview { get; set; }

		public string Comments { get; set; }
	}
}
