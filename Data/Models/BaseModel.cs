using System;

namespace Data.Models
{
    public class BaseModel
	{
		public int Id { get; set; }

		public bool IsActive { get; set; }

		public DateTime? CreatedDate { get; set; }

		public DateTime? LastModifiedDate { get; set; }

        public User CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public User LastModifiedBy { get; set; }

        public int? LastModifiedById { get; set; }
    }
}
