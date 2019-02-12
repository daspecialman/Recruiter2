using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
    public class DashboardVM
    {
        public int ApplicantId { get; set; }
        public ICollection<ApplicationListVM> Application { get; set; }
    }
}