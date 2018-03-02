using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface IStudentServices
	{
		Student GetStudent(string userName);
	}
}
