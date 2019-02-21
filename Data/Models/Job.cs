using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Data.Models
{
    public class Job : BaseModel
    {
        //[Display(Name = "Job Id")]
        //[Required]
        //public int JobId { get; set; }

        [Display(Name = "Department")]
        public Department Department { get; set; }

		public int? DepartmentId { get; set; }

		[Display(Name = "Job Title")]
        [Required]
        public string Title { get; set; }

		

        [Display(Name = "Job Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Key Responsibilities")]
        [Required]
        public string Responsibility { get; set; }

        [Display(Name = "Personal Characteristics")]
        [Required]
        public string Characteristics { get; set; } //Text Area

        [Display(Name = "Skills Required")]
        [Required]
        public SkillLevel SkillSet { get; set; } //Text Area

        [Display(Name = "Minimum Qualification")]
        [Required]
        public MinimumQualificationType MinimumQualification { get; set; }

        [Display(Name = "Experience Level")]
        [Required]
        public ExperienceLevelType ExperienceLevel { get; set; }
        [Display(Name = "Year(s) of Experience")]
        [Range(0, 50)]
        [Required]
        public int ExperienceLength { get; set; }

        [Display(Name = "Contract Class")]
        [Required]
        public ContractClassType ContractClass { get; set; }

		[Display(Name = "Expiry Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        public bool IsApproved { get; set; }

        public bool IsPublished { get; set; }

    }
}