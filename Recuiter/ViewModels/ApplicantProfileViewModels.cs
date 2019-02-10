using System;
using Data.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruiter.ViewModels
{
	public class ApplicantProfileViewModels
	{
		public int Id { get; set; }

		[DisplayName("First Name")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		public int DocumentId { get; set; }


		//public Guid ActivationCode { get; set; }

		[DisplayName("Phone Number")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Display(Name = "E-mail Address")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "E-mail Address is required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Display(Name = "Country")]
		public string Country { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

		[DisplayName("Complete Address")]
		public string CompleteAddress { get; set; }

        [DisplayName("Job Title")]
        public string JobTitle { get; set; }

        [DisplayName("Specialization")]
        public string Specialization { get; set; }

        [DisplayName("Experience")]
		public ExperienceLevelType  ExperienceLevel { get; set; }

        [DisplayName("Year(s) of Experience")]
        public int YearsOfExperience { get; set; }

        public int Age { get; set; }

		public string Language { get; set; }

		public List<string> LanguageList => Language.Split(',').ToList();

		[DisplayName("Education Level")]
		public MinimumQualificationType EducationLevel { get; set; }

		public string Bio { get; set; }

		public List<ApplicantDocumentViewModel> Certificates { get; set; }

		[Display(Name = "Image Title")]
		public string Title { get; set; }

		[DisplayName("Upload File")]
		public string ImagePath { get; set; }

		public User User { get; set; }

		public int UserId { get; set; }
		
		[NotMapped]
		public HttpPostedFileBase ImageFile { get; set; }


		public List<Education> PastEducation { get; set; }

		public List<Experience> WorkExperience { get; set; }

		public List<ApplicantDocument> ApplicantDocuments { get; set; }

		public List<Skill> Skills { get; set; }

		public List<Education> Educations { get; set; }

		public List<Institution> Institutions { get; set; }

		public string Achievement { get; set; }

		public string FilePath { get; set; }

		public string Name { get; set; }
		
		public FileType Type { get; set; }
	}

}