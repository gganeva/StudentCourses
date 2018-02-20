using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentCourses.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentCourses.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class User : IdentityUser, IDeletable, IAuditable
	{
		private ICollection<Course> _courses;

		public User()
		{
			_courses = new HashSet<Course>();
		}

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? CreatedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? ModifiedOn { get; set; }

		public virtual ICollection<Course> Courses
		{
			get
			{
				return _courses;
			}

			set
			{
				_courses = value;
			}
		}
	}
}