using System.Linq;
using StudentCourses.Models;

namespace StudentCourses.Services
{
	public interface ICoursesServices
	{
		IQueryable<Course> GetAll();
	}
}