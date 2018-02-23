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
		#region Constants

		public const string FirstNameClaimType = "FirstName";
		public const string LastNameClaimType = "LastName";

		#endregion  // Constants 

		#region Fields

		private ICollection<Course> _courses;

		#endregion // Fields

		#region Constructor

		public User()
		{
			_courses = new HashSet<Course>();
		}

		#endregion // Constructor

		#region IDeletable  Members

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }

		#endregion  // IDeletable Members

		#region IAuditable Members

		[DataType(DataType.DateTime)]
		public DateTime? CreatedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? ModifiedOn { get; set; }

		#endregion // IAuditable Members

		#region Public Members

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

			// Add custom user claims here

			// Add the first and last names so that the _LoginPartial can pick it up;
			userIdentity.AddClaim(new Claim(FirstNameClaimType, FirstName));
			userIdentity.AddClaim(new Claim(LastNameClaimType, LastName));

			return userIdentity;
		}

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
		
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Required]
		public string FacultyNumber { get; set; }

		#endregion // Public Members
	}
}