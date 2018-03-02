using System;
using System.Collections.Generic;
using System.Linq;

using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;

namespace StudentCourses.Services
{
	public class CoursesServices : ICoursesServices
	{
		private readonly IGenericRepository<Course> _dbContextWrapper;

		public CoursesServices(IUnitOfWork unitOfWork)
		{
			_dbContextWrapper = unitOfWork.Repository<Course>();
		}

		public IEnumerable<Course> AllNonDeleted
		{
			get
			{
				return _dbContextWrapper.AllNonDeleted.ToList();
			}
		}

		public IEnumerable<Course> AllPlusDeleted
		{
			get
			{
				return _dbContextWrapper.AllAndDeleted.ToList();
			}
		}

		public Course GetCourse(Guid id)
		{
			return _dbContextWrapper.GetById(id);
		}

		public void Update(Course course)
		{
			_dbContextWrapper.Update(course);
		}

		public Guid Add(Course course)
		{
			Course deletedCourse = _dbContextWrapper.AllAndDeleted
				.Where(c => c.Title == course.Title && c.Description == course.Description)
				.FirstOrDefault();

			if (deletedCourse == null)
			{
				course.Id = Guid.NewGuid();
				_dbContextWrapper.Add(course);
				return course.Id;
			}
			else
			{
				_dbContextWrapper.Add(deletedCourse);
				return deletedCourse.Id;
			}
		}

		public void Remove(Course course)
		{
			_dbContextWrapper.Remove(course);
		}
	}
}