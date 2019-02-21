using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
	public class PostedJobVM
	{
		[Key]

		public string Department { get; set; }

		[Display(Name = "Contract Class")]
		public ContractClassType ContractClass { get; set; }

		public string Status { get; set; }

		[DisplayName("Date Posted")]
		public DateTime? DatePosted { get; set; }
	}
}