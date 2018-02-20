using StudentCourses.Models.Contracts;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;

namespace StudentCourses.Data.Repository
{
	public class DbContextWrapper<T> : IDbContextWrapper<T>
		where T : class, IDeletable
	{
		private readonly StudentCoursesDbContext _context;

		public DbContextWrapper(StudentCoursesDbContext context)
		{
			_context = context;
		}

		public IQueryable<T> All
		{
			get
			{
				return _context.Set<T>().Where(x => !x.IsDeleted);
			}
		}

		public IQueryable<T> AllAndDeleted
		{
			get
			{
				return _context.Set<T>();
			}
		}

		public void Add(T entity)
		{
			DbEntityEntry entry = _context.Entry(entity);
			if (entry.State == EntityState.Detached)
			{
				_context.Set<T>().Add(entity);
			}
			else
			{
				entry.State = EntityState.Added;
			}
		}

		public void Remove(T entity)
		{
			entity.IsDeleted = true;
			entity.DeletedOn = DateTime.Now;

			DbEntityEntry entry = _context.Entry(entity);
			entry.State = EntityState.Modified;
		}

		public void Update(T entity)
		{
			DbEntityEntry entry = _context.Entry(entity);
			if (entry.State == EntityState.Detached)
			{
				_context.Set<T>().Attach(entity);
			}
			entry.State = EntityState.Modified;
		}
	}
}
