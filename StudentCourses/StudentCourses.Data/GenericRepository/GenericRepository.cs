using System;
using System.Linq;
using System.Data.Entity;

using StudentCourses.Models;
using System.Data.Entity.Infrastructure;

namespace StudentCourses.Data
{
	public class GenericRepository<T> : IGenericRepository<T>
		where T : class, IDeletable
	{
		#region Fields

		private readonly StudentCoursesDbContext _context;
		private IDbSet<T> _entities;

		#endregion  // Fields

		#region Constructor

		public GenericRepository(StudentCoursesDbContext context)
		{
			_context = context;
		}

		#endregion // Constructor

		#region Public Members

		public IQueryable<T> AllNonDeleted
		{
			get
			{
				return Entities.Where(x => !x.IsDeleted);
			}
		}

		public IQueryable<T> AllAndDeleted
		{
			get
			{
				return Entities;
			}
		}

		public void Add(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity is IAuditable)
			{
				((IAuditable)entity).CreatedOn = DateTime.Now;
			}

			Entities.Add(entity);
		}

		public void Remove(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			entity.IsDeleted = true;
			entity.DeletedOn = DateTime.Now;
		}

		public void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity is IAuditable)
			{
				((IAuditable)entity).ModifiedOn = DateTime.Now;
			}
		}

		public T GetById(Guid id)
		{
			return Entities.Find(id);
		}

		#endregion // Public Members

		#region Private Members

		private IDbSet<T> Entities
		{
			get
			{
				if (_entities == null)
				{
					_entities = _context.Set<T>();
				}
				return _entities;
			}
		}

		#endregion // Private Members
	}
}