using System;

using StudentCourses.Models;

namespace StudentCourses.Data
{
	public interface IUnitOfWork //: IDisposable
	{
		void SaveChanges();
		IGenericRepository<T> Repository<T>() where T : class, IDeletable;
	}
}