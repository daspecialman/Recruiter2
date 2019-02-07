using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
	public class ApplicantDocumentViewModel
	{
			public int Id { get; set; }

			public string Name { get; set; }

			public string FilePath { get; set; }

			public FileType Type { get; set; }
		}
	}
