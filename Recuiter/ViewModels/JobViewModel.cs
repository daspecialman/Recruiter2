using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recruiter.ViewModels
{
    public class JobViewModel
    {
        //[Display(Name = "Job Id")]
        //[Required]
        //public int JobId { get; set; }
        //[Display(Name = "Department")]
        //public Department Department { get; set; }
        //[Display(Name = "Department")]
        //[Required]
        //public int DepartmentId { get; set; }
        [Display(Name = "Job Title")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Job Summary")]
        [Required]
        //public string Summary { get; set; }
        //[Display(Name = "Job Description")]
        //[Required]
        public string Description { get; set; }
        [Display(Name = "Key Responsibilities")]
        [Required]
        public string Responsibility { get; set; }
        [Display(Name = "Personal Characteristics")]
        [Required]
        public string Characteristics { get; set; }
        [Display(Name = "Skills Required")]
        [Required]
        public SkillLevel SkillSet { get; set; }
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
        // A combination of drop-down lists of Integer and month/year. Mutually exclusive on ContractType
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        [Display(Name = "Expiry Date")]
        [Required]
        public DateTime ExpiryDate { get; set; }
        public int? CreatedById { get; set; }
        public int Id { get; set; }

        public string QualificationFormat { get; set; }
        public string ContractFormat { get; set; }
    }

    //class VacancyEditView
    //{
    //    //Disable Edit on J0bID
    //    [Display(Name = "Job Id")]
    //    [Required]
    //    public int JobId { get; set; }
    //    [Display(Name = "Department")]
    //    public Department Department { get; set; }
    //    [Display(Name = "Department")]
    //    [Required]
    //    public int DepartmentId { get; set; }
    //    [Display(Name = "Job Title")]
    //    [Required]
    //    public string Title { get; set; }
    //    [Display(Name = "Job Summary")]
    //    [Required]
    //    public string Summary { get; set; }
    //    [Display(Name = "Job Description")]
    //    [Required]
    //    public string Description { get; set; }
    //    [Display(Name = "Duties & Responsibilities")]
    //    [Required]
    //    public string Responsibility { get; set; }
    //    [Display(Name = "General Requirement")]
    //    [Required]
    //    public string GeneralRequirement { get; set; }
    //    [Display(Name = "Skill Set")]
    //    [Required]
    //    public string SkillSet { get; set; }
    //    [Display(Name = "Minimum Academic Qualification")]
    //    [Required]
    //    public MinimumQualificationType MinimumQualification { get; set; }
    //    [Display(Name = "Level of Expertise")]
    //    [Required]
    //    public ExperienceLevelType ExperienceLevel { get; set; }
    //    [Display(Name = "Year(s) of Experience")]
    //    [Range(0, 50)]
    //    [Required]
    //    public int ExperienceLength { get; set; }
    //    [Display(Name = "Contract Class")]
    //    [Required]
    //    public ContractClassType ContractClass { get; set; }
    //    [Display(Name = "Contract Length")]
    //    [Required]
    //    public string ContractLength { get; set; } // A combination of drop-down lists of Integer and month/year. Mutually exclusive on ContractType
    //    [Display(Name = "Expiry Date")]
    //    [Required]
    //    public DateTime ExpiryDate { get; set; }

    //}
}
