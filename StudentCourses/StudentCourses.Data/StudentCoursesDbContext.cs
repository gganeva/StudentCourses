using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

using StudentCourses.Models;

namespace StudentCourses.Data
{
	public class StudentCoursesDbContext : IdentityDbContext<Student>
	{
		public StudentCoursesDbContext()
			: base("SCDB", throwIfV1Schema: false)
		{
		}

		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IDeletable
		{
			return base.Set<TEntity>();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Student>().ToTable("Students");
			modelBuilder.Entity<StudentCourse>().ToTable("StudentCourses");

		}

		public static StudentCoursesDbContext Create()
		{
			return new StudentCoursesDbContext();
		}
	}
}
