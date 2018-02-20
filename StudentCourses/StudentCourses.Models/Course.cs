using System.ComponentModel.DataAnnotations;

namespace StudentCourses.Models
{
	public class Course : DataModelBase
	{
		[Required]
		[MaxLength(150)]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }
	}
}
