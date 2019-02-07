using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Data.Models
{
	public class UserRole : BaseModel
	{
		public User User { get; set; }
        [Key(), Column(Order = 1)]
        public int UserId{ get; set; }
		public Role Role { get; set; }
        [Key(), Column(Order = 2)]
        public int RoleId { get; set; }
	}
}