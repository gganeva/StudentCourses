using StudentCourses.Data.Repository;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace StudentCourses.Services
{
	public class StudentCoursesService : IStudentCoursesService
	{
		private readonly IDbContextWrapper<StudentCourse> _dbWrapper;

		public StudentCoursesService(IDbContextWrapper<StudentCourse> dbWrapper)
		{
			_dbWrapper = dbWrapper;
		}

		public ICollection<Course> GetRegisteredCourses()
		{
			return _dbWrapper.All().Where
		}

		public ICollection<Course> UnregisteredCourses { get; set; }
	}
}
