using StudentCourses.Models;
using StudentCourses.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentCourses.Web.Tests.MockServices
{
	public class MockCoursesServices : ICoursesServices
	{
		IEnumerable<Course> ICoursesServices.AllNonDeleted => throw new NotImplementedException();

		IEnumerable<Course> ICoursesServices.AllPlusDeleted => throw new NotImplementedException();

		public IQueryable<Course> GetAll()
		{
			return new List<Course>(new Course[]
			{
				// TODO 
			}).AsQueryable();
		}

		public Course GetCourse(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(Course course)
		{
			throw new NotImplementedException();
		}

		Guid ICoursesServices.Add(Course course)
		{
			throw new NotImplementedException();
		}

		Course ICoursesServices.GetCourse(Guid id)
		{
			throw new NotImplementedException();
		}

		void ICoursesServices.Remove(Course course)
		{
			throw new NotImplementedException();
		}

		void ICoursesServices.Update(Course course)
		{
			throw new NotImplementedException();
		}
	}
}
