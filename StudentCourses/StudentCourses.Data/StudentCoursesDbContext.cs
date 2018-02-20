using Microsoft.AspNet.Identity.EntityFramework;
using StudentCourses.Models;
using System.Data.Entity;

namespace StudentCourses.Data
{
	public class StudentCoursesDbContext : IdentityDbContext<User>
	{
		public StudentCoursesDbContext()
			: base("SCDB", throwIfV1Schema: false)
		{
		}

		public IDbSet<Course> Courses { get; set; }
		public IDbSet<StudentCourse> StudentCourses { get; set; }

		public static StudentCoursesDbContext Create()
		{
			return new StudentCoursesDbContext();
		}
	}

}
