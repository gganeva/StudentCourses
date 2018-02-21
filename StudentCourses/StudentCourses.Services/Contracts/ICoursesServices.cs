﻿using System.Linq;
using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface ICoursesServices
	{
		IQueryable<Course> GetAll();
	}
}