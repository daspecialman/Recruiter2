using Data.Enums;
using System;
using System.Collections.Generic;

namespace Recruiter.ViewModels
{
    public class ApplicationVM
    {
        public List<int>Id { get; set; }

        public string  JobTitle { get; set; }

        public DateTime? Date { get; set; }

        public JobApplicationWorkFlow Status { get; set; }
    }
}