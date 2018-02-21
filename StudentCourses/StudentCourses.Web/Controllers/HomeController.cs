using StudentCourses.Services;
using StudentCourses.Web.Models.Home;
using System.Linq;
using System.Web.Mvc;

namespace StudentCourses.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICoursesServices _coursesServices;

		public HomeController(ICoursesServices coursesServices)
		{
			_coursesServices = coursesServices;
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

				var viewModel = new HomeViewModel()
				{
					AllCourses = courses,
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