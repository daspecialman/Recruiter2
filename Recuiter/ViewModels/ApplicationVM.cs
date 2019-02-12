using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;

namespace Recruiter.ViewModels
{
    public class ApplicationListVM
    {
        public List<ApplicationVM>AppliedJobs{ get; set; }
    }

	public class ApplicationVM
	{
        public int Id { get; set; }

        public string JobTitle { get; set; }

		public DateTime? Date { get; set; }

		public JobApplicationWorkFlow Status { get; set; }
	}
}