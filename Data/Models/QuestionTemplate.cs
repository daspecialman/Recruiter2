using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class QuestionTemplate : BaseModel
	{

		public string Name { get; set; }

		public Department Department { get; set; }

		public int? DepartmentId { get; set; }
	}
}
