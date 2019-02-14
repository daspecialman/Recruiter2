using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
	public enum JobApplicationWorkFlow
	{
		Requested = 1,

        [Display(Name="Awaiting Review")]
		AwaitingHRReview,

        [Display(Name = "Awaiting Review")]
        AwaitingHODReview,

        [Display(Name = "Interview Schedule")]
        InterviewSchedule,

        [Display(Name = "Interview Schedule")]
        InterviewRequest,
		InterviewProcess,
		Successfull,
		Archived,
		Rejected
	}

    public enum AppliedJobStatus
    {
        [Display(Name = "Accepted")]
        Accepted = 1,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Declined")]
        Declined
    }

}


  


