using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
	public class TotalApplicantAppliedVM
	{
		[Key]

		[DisplayName("FirstName")]
		public string FirstName { get; set; }

		[DisplayName("LastName")]
		public string LastName { get; set; }

		[DisplayName("Department")]
		public string Department { get; set; }

		[DisplayName("Email Address")]
		public	string	Email { get; set; }

		[DisplayName("Job Title")]
		public string JobTitle { get; set; }

		[DisplayName("Phone Number")]
		public string PhoneNumber { get; set; }

		[DisplayName("Date Applied")]
		public DateTime? DateApplied { get; set; }
	}
}