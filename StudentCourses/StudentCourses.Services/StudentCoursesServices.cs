using StudentCourses.Data.Repository;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;
using System.Linq;

namespace StudentCourses.Services
{
	public class StudentCoursesServices : IStudentCoursesServices
	{
		private readonly IDbContextWrapper<StudentCourse> _dbContextWrapper;

		public StudentCoursesServices(IDbContextWrapper<StudentCourse> dbContextWrapper)
		{
			_dbContextWrapper = dbContextWrapper;
		}

		public IQueryable<StudentCourse> GetAll()
		{
			return _dbContextWrapper.All;
		}
	}
}