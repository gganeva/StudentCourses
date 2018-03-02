using StudentCourses.Data;
using StudentCourses.Data.Migrations;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StudentCourses.Web
{
	public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
			Database.SetInitializer<StudentCoursesDbContext>(
				new MigrateDatabaseToLatestVersion<StudentCoursesDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
