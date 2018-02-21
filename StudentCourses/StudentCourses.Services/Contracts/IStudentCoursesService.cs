using System.Collections.Generic;
using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface IStudentCoursesService
	{
		ICollection<Course> RegisteredCourses { get; set; }
		ICollection<Course> UnregisteredCourses { get; set; }
	}
}