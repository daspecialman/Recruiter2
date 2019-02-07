using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class ApplicantInterview : BaseModel
	{
		public Application Application { get; set; }

		public int ApplicationId { get; set; }

		public Interview Interview { get; set; }

		public int InterviewId { get; set; }

		public DateTime InterviewTime { get; set; }
		
		public ICollection<ApplicationReview> ApplicationReviews { get; set; }
	}
}
