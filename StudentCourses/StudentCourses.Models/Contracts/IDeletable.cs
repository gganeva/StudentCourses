using System;

namespace StudentCourses.Models
{
	public interface IDeletable
	{ 
		bool IsDeleted { get; set; }
		DateTime? DeletedOn { get; set; }
	}
}
