using StudentCourses.Data.Repository;
using StudentCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourses.Services
{
	public class CoursesServices : ICoursesServices
	{
		private readonly IDbContextWrapper<Course> _dbContextWrapper;

		public CoursesServices(IDbContextWrapper<Course> dbContextWrapper)
		{
			_dbContextWrapper = dbContextWrapper;
		}

		public IQueryable<Course> GetAll()
		{
			return _dbContextWrapper.All;
		}
	}
}
