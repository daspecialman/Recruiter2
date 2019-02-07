using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User
	{
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public User CreatedBy { get; set; }

        public int? CreatedById { get; set; }

        public User LastModifiedBy { get; set; }

        public int? LastModifiedById { get; set; }

        public bool IsDeleted { get; set; }

        public string Username { get; set; }

		[Display(Name = "First Name")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[Display(Name = "E-mail Address")]
		[DataType(DataType.EmailAddress)]
		[Required(AllowEmptyStrings = false, ErrorMessage = "E-mail Address is required")]
		public string Email { get; set; }

		public string ImagePath { get; set; }

		[Display(Name = "Password")]
		[DataType(DataType.Password)]
        public string Password { get; set; }

		public string ProfilePictureUrl { get; set; }

		public bool IsActive { get; set; }

		public Department Department { get; set; }

		public int? DepartmentId { get; set; }

		public virtual ICollection<UserRole> Roles { get; set; }

		public string ResetPasswordCode { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    
}