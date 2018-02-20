using StudentCourses.Models.Contracts;
using System.Linq;

namespace StudentCourses.Data.Repository
{
	public interface IDbContextWrapper<T> where T : class, IDeletable
	{
		IQueryable<T> All { get; }
		IQueryable<T> AllAndDeleted { get; }

		void Add(T entity);
		void Remove(T entity);
		void Update(T entity);
	}
}