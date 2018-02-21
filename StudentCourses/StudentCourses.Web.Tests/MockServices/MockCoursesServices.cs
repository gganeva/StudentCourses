using StudentCourses.Models;
using StudentCourses.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentCourses.Web.Tests.MockServices
{
	public class MockCoursesServices : ICoursesServices
	{
		public IQueryable<Course> GetAll()
		{
			return new List<Course>(new Course[]
			{
				// TODO 
			}).AsQueryable();
		}
	}
}
