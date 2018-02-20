using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentCourses.Web.Startup))]
namespace StudentCourses.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
