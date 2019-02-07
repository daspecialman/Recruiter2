using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
    public class UserVM
    {
        
        [Required ]
        public string Username { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

		public HttpPostedFileBase file { get; set; }
		public string ImagePath { get; set; }

		[Required]
        public string Password { get; set; }

		[Required]
		public Guid ActivationCode { get; set; }


	}
}
