using Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Recuiter.Context.Map
{
	public class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap()
		{
			HasOptional(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).WillCascadeOnDelete(false);

			HasOptional(x => x.LastModifiedBy).WithMany().HasForeignKey(x => x.LastModifiedById).WillCascadeOnDelete(false);
		}
	}


	public class RoleMap : EntityTypeConfiguration<Role>
	{
		public RoleMap()
		{
			HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
		}
	}

	public class UserRoleMap : EntityTypeConfiguration<UserRole>
	{
		public UserRoleMap()
		{
			HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasRequired(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);
			HasRequired(x => x.User).WithMany().HasForeignKey(y=> y.UserId).WillCascadeOnDelete(false);
			HasRequired(x => x.Role).WithMany().HasForeignKey(y => y.RoleId).WillCascadeOnDelete(false);
		}
	}


	public class ApplicantDocumentMap : EntityTypeConfiguration<ApplicantDocument>
	{
		public ApplicantDocumentMap()
		{

		}
	}

  public class DepartmentMap : EntityTypeConfiguration<Department>
	{
		public DepartmentMap()
		{
			HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.HoD).WithMany().WillCascadeOnDelete(false);
		}

	}
	public class ApplicantMap : EntityTypeConfiguration<Applicant>
	{
		public ApplicantMap()
		{
			HasOptional(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);
			HasRequired(x => x.User).WithMany().WillCascadeOnDelete(false);
		}
	}

	public class ApplicationMap : EntityTypeConfiguration<Application>
	{
		public ApplicationMap()
		{
			HasOptional(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);

		}
	}

	public class JobMap : EntityTypeConfiguration<Job>
	{
		public JobMap()
		{
			HasOptional(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);

		}
	}
	public class ApplicationReviewMap : EntityTypeConfiguration<ApplicationReview>
	{
		public ApplicationReviewMap()
		{
			HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);
			HasRequired(x => x.User).WithMany().WillCascadeOnDelete(false);
		}
	}


	public class ApplicationReviewAssesmentMap : EntityTypeConfiguration<ApplicantReviewAssessment>
	{
		public ApplicationReviewAssesmentMap()
		{
			HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);
		}
	}

	public class InterViewQuestionMap : EntityTypeConfiguration<InterviewQuestion>
	{
		public InterViewQuestionMap()
		{
			HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);
		}
	}

	public class ReviewResultMap : EntityTypeConfiguration<ReviewResult>
	{
		public ReviewResultMap()
		{
			HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
			HasOptional(x => x.LastModifiedBy).WithMany().WillCascadeOnDelete(false);
		}
	}
}