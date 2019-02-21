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

        public ContractClassType[] JobType { get; set; }

		public ExperienceLevelType[] Experience { get; set; }

        public int? page { get; set; }

    }

}
