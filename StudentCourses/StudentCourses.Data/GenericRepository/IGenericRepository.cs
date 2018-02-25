using System;
using System.Linq;

using StudentCourses.Models;

namespace StudentCourses.Data
{
	public interface IGenericRepository<T> where T : class, IDeletable
	{
		IQueryable<T> AllNonDeleted { get; }
		IQueryable<T> AllAndDeleted { get; }

		void Add(T entity);
		void Remove(T entity);
		void Update(T entity);
		T GetById(Guid id);
	}
}