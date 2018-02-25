using StudentCourses.Models;

namespace StudentCourses.Services.Contracts
{
	public interface IUserServices
	{
		Student GetUser(string userName);
	}
}
