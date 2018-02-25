using System;
using System.Linq;
using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface ICoursesServices
	{
		IQueryable<Course> GetAll();

		Course GetCourse(Guid id);

		void Update(Course course);
	}
}