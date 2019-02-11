using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;

namespace Recruiter.ViewModels
{
    public class ApplicationVM
    {
        public List<Application>AppliedJobs{ get; set; }
    }

	public class Application
	{
		public string JobTitle { get; set; }

		public DateTime? Date { get; set; }

		public JobApplicationWorkFlow Status { get; set; }
	}
}