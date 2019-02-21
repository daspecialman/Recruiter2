using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{

	public class Search
	{
		public int Id { get; set; }
        public string SkillSearch { get; set; }

        public int[] JobType { get; set; }

		public int[] Experience { get; set; }
	}

}
