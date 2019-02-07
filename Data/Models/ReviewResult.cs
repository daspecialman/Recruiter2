using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ReviewResult : BaseModel
    {
        public User Reviewer { get; set; }
        [Key(), Column(Order = 1)]
        public int ReviewerId { get; set; }
        public Applicant Applicant { get; set; }
        [Key(), Column(Order = 2)]
        public int ApplicantId { get; set; }
        public int Appearance { get; set; }
        public int Disposition { get; set; }
        public int Communication { get; set; }
        public int EducationalQualification { get; set; }
        public int RelevantExperience { get; set; }
        public int RelevantTechnicalExperience { get; set; }
        public int AnalyticalReasoningAbility { get; set; }
        public int GeneralKnowledge { get; set; }
        public int EstimateOfIntelligence { get; set; }
        public string GeneralRemark { get; set; }
        public string Recommendation { get; set; }
        public int TotalScore { get; set; }
    }
}
