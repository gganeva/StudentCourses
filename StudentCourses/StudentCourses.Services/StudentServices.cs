using System.Linq;

using StudentCourses.Data;
using StudentCourses.Models;
using StudentCourses.Services.Contracts;

namespace StudentCourses.Services
{
	public class StudentServices : IStudentServices
	{
		private readonly IGenericRepository<Student> _dbContextWrapper;

		public StudentServices(IUnitOfWork unitOfWork)
		{
			_dbContextWrapper = unitOfWork.Repository<Student>();
		}

		public Student GetStudent(string userName)
		{
			return _dbContextWrapper.AllNonDeleted.Where(x => x.UserName == userName).FirstOrDefault();
		}
	}
}