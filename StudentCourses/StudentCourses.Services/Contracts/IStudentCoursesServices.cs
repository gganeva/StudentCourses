using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface IStudentCoursesServices
	{
		IEnumerable<StudentCourse> AllNonDeleted(Expression<Func<StudentCourse, bool>> exp);
		IEnumerable<StudentCourse> AllAndDeleted(Expression<Func<StudentCourse, bool>> exp);

		Guid Add(StudentCourse stCourse);
		void Remove(StudentCourse stCourse);
		void Update(StudentCourse stCourse);
	}
}