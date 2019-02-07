using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models

{
	//public class ApplicantResume
	//{ 
	//public ICollection<Education> Education { get; set; }
	//public ICollection<Experience> Experience { get; set; }
	//public ICollection<Skill> Skill { get; set; }
}



public class Education
	{
		public int Id { get; set; }

		public string Qualification { get; set; }

		public DateTime FromDate { get; set; }

		public DateTime ToDate { get; set; }

		public string Institution { get; set; }

		public string CourseStudied { get; set; }

	}

	public class Experience
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public DateTime FromDate { get; set; }

		public DateTime ToDate { get; set; }

		public bool IsPresent { get; set; }

		public string CompanyName { get; set; }
	}



	public class Skill
	{
		public int Id { get; set; }

		public SkillLevel Skilllevel { get; set; }

		public string Achievement { get; set; }

	}


