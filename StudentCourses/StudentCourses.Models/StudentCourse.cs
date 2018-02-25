using System;

namespace StudentCourses.Models
{
	/// <summary>
	/// POCO class mapping a student to a course.
	/// </summary>
	public class StudentCourse : DataModelBase
	{
		public string StudentId { get; set; }
		public virtual Student Student { get; set; }

		public Guid CourseId { get; set; }
		public virtual Course Course { get; set; }
	}
}