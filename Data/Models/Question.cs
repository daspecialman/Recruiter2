using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class Question : BaseModel
	{
		public QuestionTemplate QuestionTemplate { get; set; }

		public int QuestionTemplateId { get; set; }

		public string QuestionText { get; set; }

		public bool IsTextResponse { get; set; }
	}
}
