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
    [Description("Masters Degree")]
    MastersDegree = 1,
    [Description("Bachelor Degree")]
    BachelorDegree,
    [Description("Higher National Diploma")]
    HigherNationalDiploma,
    [Description("National Certificate of Education")]
    NationalCertificateOfEducation,
    [Description("Ordinary National Diploma")]
    OrdinaryNationalDiploma,
    [Description("Senior School Certificate")]
    SeniorSchoolCertificate
}

public enum ExperienceLevelType
{
    [Description("Expert")]
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
    [Description("Full Time")]
    Fulltime = 1,
    [Description("Internship")]
    Internship,
    [Description("Contract")]
    Contract
}


