using AutoMapper;
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
		private readonly IMapper _mapper;

		public HomeController(
			ICoursesServices coursesServices,
			IStudentCoursesServices studentCourseServices,
			IMapper mapper)
		{
			_coursesServices = coursesServices;
			_studentCoursesServices = studentCourseServices;
			_mapper = mapper;
		}

		public ActionResult Index()
		{
			if (Request.IsAuthenticated)
			{
				var courses = _coursesServices.GetAll()
					.ToList()
					.Select(x => _mapper.Map<CourseViewModel>(x))
					.ToList();

				var studentCourses = _studentCoursesServices.GetAll()
					.Where(x => x.User.UserName == System.Web.HttpContext.Current.User.Identity.Name)
					.ToList()
					.Select(x => _mapper.Map<CourseViewModel>(x.Course))
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