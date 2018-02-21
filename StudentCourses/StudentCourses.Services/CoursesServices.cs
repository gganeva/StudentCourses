﻿using StudentCourses.Data.Repository;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;
using System.Linq;

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
