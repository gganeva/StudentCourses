using System;

namespace StudentCourses.Models.Contracts
{
	public interface IAuditable
	{
		DateTime? CreatedOn { get; set; }
		DateTime? ModifiedOn { get; set; }
	}
}