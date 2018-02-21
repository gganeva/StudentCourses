using StudentCourses.Services.Contracts;
using StudentCourses.Web.Models.Home;
using System.Linq;
using System.Web.Mvc;

namespace StudentCourses.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICoursesServices _coursesServices;
		private readonly IStudentCoursesServices _studentCoursesServices;


		public HomeController(
			ICoursesServices coursesServices,
			IStudentCoursesServices studentCourseServices)
		{
			_coursesServices = coursesServices;
			_studentCoursesServices = studentCourseServices;
		}

		public ActionResult Index()
		{
			if (Request.IsAuthenticated)
			{
				var courses = _coursesServices.GetAll()
					.Select(x => new CourseViewModel()
					{
						Title = x.Title,
						Description = x.Description
					})
					.ToList();

				var studentCourses = _studentCoursesServices.GetAll()
					.Where(x => x.User.UserName == System.Web.HttpContext.Current.User.Identity.Name)
					.Select(x => new CourseViewModel()
					{
						Title = x.Course.Title,
						Description = x.Course.Description
					})
					.ToList();

				var viewModel = new HomeViewModel()
				{
					AllCourses = courses,
					RegisteredCourses = studentCourses
				};

				return View(viewModel);
			}
			else
			{
				return RedirectToAction("Login", "Account");
			}
		}
	}
}