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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

		[Display(Name = "E-mail Address")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Your email is required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter country")]
        public string Country { get; set; }

        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter city")]
        public string City { get; set; }

		[DisplayName("Complete Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please input address")]
        public string CompleteAddress { get; set; }

        [DisplayName("Job Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your job title")]
        public string JobTitle { get; set; }

        [DisplayName("Specialization")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter specialization e.g. PHP")]
        public string Specialization { get; set; }

        [DisplayName("Experience")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select your experience level")]
        public ExperienceLevelType  ExperienceLevel { get; set; }

        [DisplayName("Year(s) of Experience")]
        public int YearsOfExperience { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Your age is required")]
        public int Age { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "What languages do you speak?")]
        public string Language { get; set; }

		public List<string> LanguageList => Language.Split(',').ToList();

		[DisplayName("Education Level")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select highest education level")]
        public MinimumQualificationType EducationLevel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please tell us about yourself")]
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


		public List<EducationVM> PastEducation { get; set; }

		public List<Experience> WorkExperience { get; set; }

		public List<ApplicantDocument> ApplicantDocuments { get; set; }

		public List<Skill> Skills { get; set; }

		public List<EducationVM> Educations { get; set; }

		public List<Institution> Institutions { get; set; }

		public string Achievement { get; set; }

		public string FilePath { get; set; }

		public string Name { get; set; }
		
		public FileType Type { get; set; }
	}

}