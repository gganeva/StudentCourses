using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentCourses.Models
{
	/// <summary>
	/// POCO class for a course in the system.
	/// </summary>
	public class Course : DataModelBase
	{
		[Required]
		[MaxLength(150)]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }
	}
}