using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class InterviewReviewer : BaseModel
	{
		
		public int InterviewId { get; set; }

		public Interview Interview { get; set; }


	}
}
