﻿using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Web.Models.Home;

namespace StudentCourses.Web.Controllers
{
	public class HomeController : Controller
	{
		#region Fields

		private readonly IGenericRepository<StudentCourse> _studentCourses;
		private readonly IGenericRepository<Course> _courses;
		private readonly IGenericRepository<Student> _students;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		#endregion // Fields

		#region Constructor

		public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_studentCourses = _unitOfWork.Repository<StudentCourse>();
			_courses = _unitOfWork.Repository<Course>();
			_students = _unitOfWork.Repository<Student>();
			_mapper = mapper;
		}

		#endregion // Constructor

		#region Public Members

		public ActionResult Index()
		{
			if (Request.IsAuthenticated)
			{
				var courses = _courses.AllNonDeleted
					.ProjectTo<CourseViewModel>()
					.ToList();

				var studentCourses = _studentCourses.AllNonDeleted
					.Where(x => x.Student.UserName == User.Identity.Name)
					.Select(x => x.Course)
					.ProjectTo<CourseViewModel>()
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
				return RedirectToAction(nameof(AccountController.Login), "Account");
			}
		}

		public ActionResult EditCourse(Guid? id)
		{
			if (!Request.IsAuthenticated)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Unauthenticated attempt to edit a course is detected!");
			}

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Invalid course id!");
			}

			var course = _courses.GetById(id.Value);

			if (course == null)
			{
				return new HttpNotFoundResult("No such course is found!");
			}

			var viewModel = _mapper.Map<CourseViewModel>(course);
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult RegisterToCourse(Guid? id)
		{
			if (!Request.IsAuthenticated)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Unauthenticated attempt to register for course is detected!");
			}

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Invalid course id!");
			}

			var course = _courses.GetById(id.Value);

			if (course == null)
			{
				return new HttpNotFoundResult("The specified course is not found!");
			}

			// TODO : Check whether this can be done without this call.
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
				return new HttpStatusCodeResult(HttpStatusCode.Conflict,
					String.Format("A registration to course '{0}' already exists!", course.Title));
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

			return RedirectToAction(nameof(GetRegisteredCourses));
		}

		[HttpPost]
		public ActionResult UnregisterFromCourse(Guid? id)
		{
			if (!Request.IsAuthenticated)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Unauthenticated attempt to unregister from course is detected!");
			}

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Invalid course id!");
			}

			var studentCourseToRemove = _studentCourses.AllNonDeleted
				.Where(x => x.Student.UserName == User.Identity.Name && x.Course.Id == id.Value)
				.FirstOrDefault();

			if (studentCourseToRemove == null)
			{
				return new HttpNotFoundResult("No registration to the specified course exists!");
			}

			_studentCourses.Remove(studentCourseToRemove);
			_unitOfWork.SaveChanges();

			return RedirectToAction(nameof(GetRegisteredCourses));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditCourse(CourseViewModel course)
		{
			if (ModelState.IsValid)
			{
				var dbCourse = _courses.GetById(course.Id);
				dbCourse.Title = course.Title;
				dbCourse.Description = course.Description;
				dbCourse.ModifiedOn = DateTime.Now;

				_courses.Update(dbCourse);
				_unitOfWork.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			return View(course);
		}

		public ActionResult AddCourse()
		{
			if (!Request.IsAuthenticated)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Unauthenticated attempt to unregister from course is detected!");
			}

			return View(new CourseViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddCourse(CourseViewModel course)
		{
			if (ModelState.IsValid)
			{
				_courses.Add(_mapper.Map<Course>(course));
				_unitOfWork.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
			return View(course);
		}

		[HttpPost]
		public ActionResult DeleteCourse(Guid? id)
		{
			if (!Request.IsAuthenticated)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Unauthenticated attempt to delete a course is detected!");
			}

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
					"Invalid course id!");
			}

			// TODO :
			Course course =_courses.GetById(id.Value);

			var studentCourses = _studentCourses.AllNonDeleted
				.Where(stCourse => stCourse.CourseId == id.Value)
				.ToList();

			foreach (StudentCourse stCourse in studentCourses)
			{
				_studentCourses.Remove(stCourse);
			}

			_courses.Remove(course);

			_unitOfWork.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult GetAllCourses()
		{
			if (Request.IsAjaxRequest())
			{
				var courses = _courses.AllNonDeleted
					.ProjectTo<CourseViewModel>()
					.ToList();
				return PartialView("_ListAllCourses", courses);
			}
			else
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
		}

		public ActionResult GetRegisteredCourses()
		{
			if (Request.IsAjaxRequest())
			{
				var studentCourses = _studentCourses.AllNonDeleted
					.Where(stCourse => stCourse.Student.UserName == User.Identity.Name)
					.Select(stCourse => stCourse.Course)
					.ProjectTo<CourseViewModel>()
					.ToList();
					
				return PartialView("_ListRegisteredCourses", studentCourses);
			}
			else
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
		}

		#endregion	// Public Members
	}
}