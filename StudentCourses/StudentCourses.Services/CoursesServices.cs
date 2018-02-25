using System;
using System.Linq;

using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;

namespace StudentCourses.Services
{
	public class CoursesServices : ICoursesServices
	{
		private readonly IGenericRepository<Course> _dbContextWrapper;

		public CoursesServices(IGenericRepository<Course> dbContextWrapper)
		{
			_dbContextWrapper = dbContextWrapper;
		}

		public IQueryable<Course> GetAll()
		{
			return _dbContextWrapper.AllNonDeleted;
		}

		public Course GetCourse(Guid id)
		{
			return _dbContextWrapper.GetById(id);
		}

		public void Update(Course course)
		{
			_dbContextWrapper.Update(course);
		}
	}
}
