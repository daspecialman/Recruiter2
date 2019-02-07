using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class WorkFlowAudit : BaseModel
	{
		public int ActionId { get; set; }

		public WorkFlowType workFlowType { get; set; }

		public int PreviousStatus { get; set; }

		public int NewStatus { get; set; }

		public string Comment { get; set; }
	}
}
