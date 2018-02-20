using System;

namespace StudentCourses.Models.Contracts
{
	public interface IDeletable
	{ 
		bool IsDeleted { get; set; }
		DateTime? DeletedOn { get; set; }
	}
}
