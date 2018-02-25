using AutoMapper;
using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Web.Models.Home;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StudentCourses.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGenericRepository<StudentCourse> _studentCourses;
		private readonly IGenericRepository<Course> _courses;
		private readonly IGenericRepository<Student> _students;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_studentCourses = _unitOfWork.Repository<StudentCourse>();
			_courses = _unitOfWork.Repository<Course>();
			_students = _unitOfWork.Repository<Student>();
			_mapper = mapper;
		}

		public ActionResult Index()
		{
			if (Request.IsAuthenticated)
			{
				var courses = _courses.AllNonDeleted
					.ToList()
					.Select(x => _mapper.Map<CourseViewModel>(x))
					.ToList();

				var studentCourses = _studentCourses.AllNonDeleted
					.Where(x => x.Student.UserName == System.Web.HttpContext.Current.User.Identity.Name)
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

		public ActionResult EditCourse(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var course = _courses.GetById(id.Value);

			if (course == null)
			{
				return new HttpNotFoundResult();
			}

			var viewModel = _mapper.Map<CourseViewModel>(course);
			return View(viewModel);
		}

		public ActionResult RegisterCourse(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var course = _courses.GetById(id.Value);

			if (course == null)
			{
				return new HttpNotFoundResult();
			}

			var student = _students.AllNonDeleted
				.Where(x => x.UserName == User.Identity.Name)
				.FirstOrDefault();

			if (student == null)
			{
				return new HttpNotFoundResult();
			}

			var studentCourse = _studentCourses.AllAndDeleted
				.Where(x => x.Student.UserName == student.UserName && x.CourseId == id.Value)
				.FirstOrDefault();

			if (studentCourse != null && !studentCourse.IsDeleted)
			{
				// TODO :
				return new ContentResult() { Content = "User already has the course" };
			}

			if (studentCourse != null)
			{
				studentCourse.IsDeleted = false;
				_studentCourses.Update(studentCourse);
			}
			else
			{
				_studentCourses.Add(new StudentCourse()
				{
					Course = course,
					Student = student
				});
			}
			_unitOfWork.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult RemoveCourse(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var course = _courses.GetById(id.Value);

			if (course == null)
			{
				return new HttpNotFoundResult();
			}

			var student = _students.AllNonDeleted
				.Where(st => st.UserName == User.Identity.Name)
				.FirstOrDefault();

			if (student == null)
			{
				return new HttpNotFoundResult();
			}

			var studentCourseToRemove = _studentCourses.AllNonDeleted
				.Where(x => x.Student.UserName == student.UserName && x.Course.Id == id.Value)
				.FirstOrDefault();

			if (studentCourseToRemove == null)
			{
				return new HttpNotFoundResult();
			}

			_studentCourses.Remove(studentCourseToRemove);
			_unitOfWork.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public ActionResult UpdateCourse(CourseViewModel course)
		{
			if (ModelState.IsValid)
			{
				var dbCourse = _courses.GetById(course.Id); 
				dbCourse.Title = course.Title;
				dbCourse.Description = course.Description;
				dbCourse.ModifiedOn = DateTime.Now;

				_courses.Update(dbCourse);
				_unitOfWork.SaveChanges();
			}

			return RedirectToAction("Index", "Home");
		}
	}
}