using System.Linq;

using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;

namespace StudentCourses.Services
{
	public class UserServices : IUserServices
	{
		private readonly IGenericRepository<Student> _dbContextWrapper;

		public UserServices(IGenericRepository<Student> dbContextWrapper)
		{
			_dbContextWrapper = dbContextWrapper;
		}

		public Student GetUser(string userName)
		{
			return _dbContextWrapper.AllNonDeleted.Where(x => x.UserName == userName).FirstOrDefault();
		}
	}
}