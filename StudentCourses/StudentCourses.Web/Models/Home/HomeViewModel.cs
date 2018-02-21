using System.Collections.Generic;

namespace StudentCourses.Web.Models.Home
{
	public class HomeViewModel
	{
		public ICollection<CourseViewModel> AllCourses { get; set; }
		public ICollection<CourseViewModel> RegisteredCourses { get; set; }
	}
}