using System.Linq;
using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface IStudentCoursesServices
	{
		IQueryable<StudentCourse> GetAll();

		void Add(StudentCourse stCourse);
		void Remove(StudentCourse stCourse);
	}
}