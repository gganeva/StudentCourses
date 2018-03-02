using System.Collections.Generic;

namespace StudentCourses.Web.Models.Home
{
	public class HomeViewModel
	{
		public IEnumerable<CourseViewModel> AllCourses { get; set; }
		public IEnumerable<CourseViewModel> RegisteredCourses { get; set; }
	}
}