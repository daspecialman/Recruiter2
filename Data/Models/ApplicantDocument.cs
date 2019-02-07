using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class ApplicantDocument : BaseModel
	{
		public Applicant Applicant { get; set; }

		public int ApplicantId { get; set; }

		public string Name { get; set; }

		public string FilePath { get; set; }

		public FileType Type { get; set; }

	}
}