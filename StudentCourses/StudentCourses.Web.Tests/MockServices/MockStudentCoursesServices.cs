using System.Collections.Generic;
using System.Linq;
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
	}
}
