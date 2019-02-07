namespace Recruiter.ViewModels
{
    public class AssessmentVM
    {
        public int ReviewerId { get; set; }
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

    }
}