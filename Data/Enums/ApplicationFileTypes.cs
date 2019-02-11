using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum FileType
{
    CoverLetter = 1,
    Cv,
    MedicalReport,
	Certificate
}

public enum MinimumQualificationType
{
    [Display(Name = "Masters Degree")]
    MastersDegree = 1,
    [Display(Name = "Bachelor Degree")]
    BachelorDegree,
    [Display(Name = "Higher National Diploma")]
    HigherNationalDiploma,
    [Display(Name = "National Certificate of Education")]
    NationalCertificateOfEducation,
    [Display(Name = "Ordinary National Diploma")]
    OrdinaryNationalDiploma,
    [Display(Name = "Senior School Certificate")]
    SeniorSchoolCertificate
}

public enum ExperienceLevelType
{
    Expert = 1,
    Intermediate,
    Beginner
}
public enum SkillLevel
{
	UI = 1,
    UX,
	Photoshop,
	AdobeXD,
    Sketch,
    Invision,
    ProjectManagement,
    HTML,
    CSS,
    MicrosoftOffice,
    Communication,
    Teamwork
}

public enum ContractClassType
{
    [Display(Name = "Full Time")]
    Fulltime = 1,
    [Display(Name = "Internship")]
    Internship,
    [Display(Name = "Contract")]
    Contract
}

