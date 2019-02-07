using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	class Profile
	{
		public int Id { get; set; }
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public Applicant Applicant { get; set; }

		public int ApplicantId { get; set; }

		public ApplicantDocument Document { get; set; }

		public int DocumentId { get; set; }

		public int ImageId { get; set; }

		[DisplayName("Phone Number")]
		public int PhoneNumber { get; set; }

		public string Email { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		[DisplayName("Complete Address")]
		public string CompleteAddress { get; set; }

		[DisplayName("Years Of Experience")]
		public int YearsOfExperience { get; set; }

		public int Age { get; set; }

		public virtual ICollection<string> Language { get; set; }

		[DisplayName("Education Level")]
		public string EducationLevel { get; set; }

		public string Bio { get; set; }
	}
}
