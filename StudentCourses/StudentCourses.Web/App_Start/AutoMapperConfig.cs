using AutoMapper;
using StudentCourses.Models;
using StudentCourses.Web.Models.Home;

namespace StudentCourses.Web.App_Start
{
	public static class AutoMapperConfig
	{
		public static IMapper MapperInstance { get; private set; }

		static AutoMapperConfig()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Course, CourseViewModel>();
			});

			MapperInstance = Mapper.Instance;
		}
	}
}