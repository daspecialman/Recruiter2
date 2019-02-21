using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models



{
    public class Applicant : BaseModel
	{
		public virtual User User { get; set; }

		public int ApplicantId { get; set; }

		public string Address { get; set; }

		[DataType(DataType.PhoneNumber)]
		[DisplayName("Phone Number")]
		public string PhoneNumber { get; set; }

		public string Country { get; set; }

		public string City { get; set; }
        
		public ExperienceLevelType ExperienceLevel { get; set; }

		public int YearsOfExperience { get; set; }

		public int Age { get; set; }

        public Job Job { get; set; }

        public string Specialization { get; set; }

        public string Languages { get; set; }

		[DisplayName("Education Level")]
		public MinimumQualificationType EducationLevel { get; set; }

		public string Bio { get; set; }
		
		public virtual ICollection<Application> Applications { get; set; }

		public virtual ICollection<Education> PastEducation { get; set; }

		public virtual ICollection<Experience> WorkExperience { get; set; }

		public virtual ICollection<ApplicantDocument> ApplicantDocuments { get; set; }

		public virtual ICollection<Skill> Skills { get; set; }

		public virtual ICollection<Institution> Institutions { get; set; }

		public string Achievement { get; set; }


	}


	public static class ApplicantExtensions {

		public static bool IsValid(this Applicant self) {
			return (!string.IsNullOrWhiteSpace(self.PhoneNumber)) ||
				!string.IsNullOrWhiteSpace(self.City);
		}
	}
}