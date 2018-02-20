namespace StudentCourses.Data.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using StudentCourses.Models;
	using System;
	using System.Data.Entity.Migrations;
	using System.Linq;

	public sealed class Configuration : DbMigrationsConfiguration<StudentCoursesDbContext>
	{
		private const string AdministratorUserName = "gali@gali.com";
		private const string AdministratorPassword = "123456";

		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			AutomaticMigrationDataLossAllowed = false;
		}

		protected override void Seed(StudentCoursesDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			SeedAdmin(context);
			SeedData(context);

			base.Seed(context);
		}

		private void SeedData(StudentCoursesDbContext context)
		{
			if (!context.Courses.Any())
			{
				for (int i = 0; i < 5; i++)
				{
					var course = new Course()
					{
						Title = String.Format("Course {0}", i + 1),
						Description = String.Format("Test course {0}", i + 1),
						CreatedOn = DateTime.Now
					};

					context.Courses.Add(course);
				}
			}

			if (!context.StudentCourses.Any())
			{
				var courses = context.Courses.Where(x => x.Title.Contains("1")).ToList();
				var user = context.Users.First();
				foreach (Course course in courses)
				{
					context.StudentCourses.Add(new StudentCourse()
					{
						Course = course,
						User = user
					});
				}
			}
		}

		private void SeedAdmin(StudentCoursesDbContext context)
		{
			if (!context.Roles.Any())
			{
				var roleStore = new RoleStore<IdentityRole>(context);
				var roleManager = new RoleManager<IdentityRole>(roleStore);
				var role = new IdentityRole() { Name = "Admin" };
				roleManager.Create(role);

				var userStore = new UserStore<User>(context);
				var userManager = new UserManager<User>(userStore);
				var user = new User()
				{
					UserName = AdministratorUserName,
					Email = AdministratorUserName,
					EmailConfirmed = true,
					CreatedOn = DateTime.Now
				};
				userManager.Create(user, AdministratorPassword);
				userManager.AddToRole(user.Id, "Admin");
			}
		}
	}
}