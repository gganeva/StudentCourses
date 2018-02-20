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

			base.Seed(context);
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