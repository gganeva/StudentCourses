using System.Linq;

using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;

namespace StudentCourses.Services
{
	public class StudentCoursesServices : IStudentCoursesServices
	{
		private readonly IGenericRepository<StudentCourse> _dbContextWrapper;

		public StudentCoursesServices(IGenericRepository<StudentCourse> dbContextWrapper)
		{
			_dbContextWrapper = dbContextWrapper;
		}

		public IQueryable<StudentCourse> GetAll()
		{
			return _dbContextWrapper.AllNonDeleted;
		}

		public void Add(StudentCourse stCourse)
		{
			_dbContextWrapper.Add(stCourse);
		}

		public void Remove(StudentCourse stCourse)
		{
			_dbContextWrapper.Remove(stCourse);
		}
	}
}