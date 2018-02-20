using Microsoft.AspNet.Identity.EntityFramework;
using StudentCourses.Models;

namespace StudentCourses.Data
{
	public class StudentCoursesDbContext : IdentityDbContext<User>
	{
		public StudentCoursesDbContext()
			: base("SCDB", throwIfV1Schema: false)
		{
		}

		public static StudentCoursesDbContext Create()
		{
			return new StudentCoursesDbContext();
		}
	}

}
