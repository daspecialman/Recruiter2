using System.ComponentModel.DataAnnotations;

namespace Recruiter.ViewModels
{
    public class DepartmentVM
    {
        [Required]
        public string Name { get; set; }
        [Display(Name ="HoD")]
        public int? HoDId { get; set; }

    }
}