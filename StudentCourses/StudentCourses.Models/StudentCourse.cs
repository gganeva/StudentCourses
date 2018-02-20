using System;

namespace StudentCourses.Models
{
	public class StudentCourse : DataModelBase
	{
		public string UserId { get; set; }
		public virtual User User { get; set; }

		public Guid CourseId { get; set; }
		public virtual Course Course { get; set; }
	}
}
