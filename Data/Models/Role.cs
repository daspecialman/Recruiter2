using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Models
{
	public class Role: BaseModel
	{
		public string Name { get; set; }

		public virtual ICollection<UserRole> Users { get; set; }
    }
}