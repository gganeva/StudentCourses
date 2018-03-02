using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;

namespace StudentCourses.Web.Tests.MockServices
{
	public class MockStudentCoursesServices : IStudentCoursesServices
	{
		public void Add(StudentCourse stCourse)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<StudentCourse> AllAndDeleted(Expression<Func<StudentCourse, bool>> exp)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<StudentCourse> AllNonDeleted(Expression<Func<StudentCourse, bool>> exp)
		{
			throw new NotImplementedException();
		}

		public IQueryable<StudentCourse> GetAll()
		{
			return new List<StudentCourse>(new StudentCourse[]
				{
					// TODO 
				}).AsQueryable();
		}

		public void Remove(StudentCourse stCourse)
		{
			throw new System.NotImplementedException();
		}

		public void Update(StudentCourse stCourse)
		{
			throw new NotImplementedException();
		}

		Guid IStudentCoursesServices.Add(StudentCourse stCourse)
		{
			throw new NotImplementedException();
		}
	}
}
