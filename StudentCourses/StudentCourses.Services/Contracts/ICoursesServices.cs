using System;
using System.Collections.Generic;
using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface ICoursesServices
	{
		IEnumerable<Course> AllNonDeleted { get; }
		IEnumerable<Course> AllPlusDeleted { get; }

		Course GetCourse(Guid id);
		void Update(Course course);
		Guid Add(Course course);
		void Remove(Course course);
	}
}