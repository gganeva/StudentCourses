using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;

namespace StudentCourses.Services
{
	public class StudentCoursesServices : IStudentCoursesServices
	{
		private readonly IGenericRepository<StudentCourse> _dbContextWrapper;

		public StudentCoursesServices(IUnitOfWork unitOfWork)
		{
			_dbContextWrapper = unitOfWork.Repository<StudentCourse>();
		}

		public IEnumerable<StudentCourse> AllNonDeleted(Expression<Func<StudentCourse, bool>> exp)
		{
			return _dbContextWrapper.AllNonDeleted.Where(exp).ToList();
		}

		public IEnumerable<StudentCourse> AllAndDeleted(Expression<Func<StudentCourse, bool>> exp)
		{
			return _dbContextWrapper.AllAndDeleted.Where(exp).ToList();
		}

		public Guid Add(StudentCourse stCourse)
		{
			StudentCourse deletedStudentCourse = _dbContextWrapper.AllAndDeleted
				.Where(s => s.CourseId == stCourse.Id && s.StudentId == stCourse.StudentId)
				.FirstOrDefault();

			if (deletedStudentCourse == null)
			{
				stCourse.Id = Guid.NewGuid();
				_dbContextWrapper.Add(stCourse);
				return stCourse.Id;
			}
			else
			{
				_dbContextWrapper.Add(deletedStudentCourse);
				return deletedStudentCourse.Id;
			}
		}

		public void Remove(StudentCourse stCourse)
		{
			_dbContextWrapper.Remove(stCourse);
		}

		public void Update(StudentCourse stCourse)
		{
			_dbContextWrapper.Update(stCourse);
		}
	}
}