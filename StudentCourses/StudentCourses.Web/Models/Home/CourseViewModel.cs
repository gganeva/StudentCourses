using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudentCourses.Web.Models.Home
{
	public class CourseViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Course title cannot be empty!")]
		[StringLength(150, ErrorMessage = "The maximum length of title is 150!")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Course description cannot be empty!")]
		public string Description { get; set; }
	}
}