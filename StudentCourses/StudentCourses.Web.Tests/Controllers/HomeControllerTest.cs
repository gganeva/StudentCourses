using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentCourses.Services.Contracts;
using StudentCourses.Web;
using StudentCourses.Web.Controllers;
using StudentCourses.Web.Tests.MockServices;

namespace StudentCourses.Web.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
			// Arrange
			HomeController controller = new HomeController(null, null);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
