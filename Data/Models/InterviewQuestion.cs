using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Models
{
	public class InterviewQuestion : BaseModel
	{
		public Department Department { get; set; }

		public int? DepartmentId { get; set; }

		public string Question { get; set; }

		public bool IsGeneral { get; set; }
		//public string ResponseType { get; set; }

	}
}