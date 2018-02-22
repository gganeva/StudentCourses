[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(StudentCourses.Web.App_Start.NinjectConig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(StudentCourses.Web.App_Start.NinjectConig), "Stop")]

namespace StudentCourses.Web.App_Start
{
    using System;
	using System.Data.Entity;
	using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
	using Ninject.Web.Common.WebHost;
	using Ninject.Extensions.Conventions;
	using StudentCourses.Data;
	using StudentCourses.Data.Repository;
	using StudentCourses.Services.Contracts;
	using AutoMapper;

	public static class NinjectConig 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
			kernel.Bind(x =>
			{
				x.FromAssemblyContaining<IService>()
				.SelectAllClasses()
				.BindDefaultInterface();
			});

			kernel.Bind(typeof(DbContext)).To(typeof(StudentCoursesDbContext)).InRequestScope();
			kernel.Bind(typeof(IDbContextWrapper<>)).To(typeof(DbContextWrapper<>));
			kernel.Bind<IMapper>().ToMethod(ctx => AutoMapperConfig.MapperInstance);
        }        
    }
}