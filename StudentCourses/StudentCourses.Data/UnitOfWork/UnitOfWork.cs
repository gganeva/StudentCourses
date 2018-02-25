using StudentCourses.Models;
using System;
using System.Collections.Generic;

namespace StudentCourses.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		#region Fields

		//private bool _disposed;
		private readonly StudentCoursesDbContext _context;
		private Dictionary<string, object> _repositories;

		#endregion    // Fields

		#region Constructor

		public UnitOfWork(StudentCoursesDbContext context)
		{
			_context = context;
		}

		#endregion  // Constructor

		#region IUnitOfWork Members

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IGenericRepository<T> Repository<T>() where T : class, IDeletable
		{
			if (_repositories == null)
			{
				_repositories = new Dictionary<string, object>();
			}

			var type = typeof(T).Name;

			if (!_repositories.ContainsKey(type))
			{
				var repositoryType = typeof(GenericRepository<>);
				var repositoryInstance = Activator.CreateInstance(
					repositoryType.MakeGenericType(typeof(T)), _context);
				_repositories.Add(type, repositoryInstance);
			}
			return (GenericRepository<T>)_repositories[type];
		}

		#endregion // IUnitOfWork Members

		//#region IDisposable Pattern

		///// <summary>
		///// Finalizer of <see cref="UnitOfWork"/>.
		///// </summary>
		//~UnitOfWork()
		//{
		//	// handler that alerts to failure to follow the rules
		//	DisposeNotCalled();

		//	// ‘false’ indicates that Dispose() is called as a result
		//	// of garbage collection, not Dispose().
		//	Dispose(false);
		//}

		///// <summary>
		///// Performs application-defined tasks associated with freeing,
		///// releasing, or resetting unmanaged resources.
		///// </summary>
		//public void Dispose()
		//{
		//	Dispose(true);

		//	//take this object off the finalization queue of the GC
		//	GC.SuppressFinalize(this);
		//}

		///// <summary>
		///// Dispose(bool disposing) executes in two distinct scenarios.
		///// If disposing equals true, the method has been called directly
		///// or indirectly by a user's code. Managed and unmanaged resources
		///// can be disposed.
		///// If disposing equals false, the method has been called by the 
		///// runtime from inside the finalizer and you should not reference 
		///// other objects. Only unmanaged resources can be disposed.
		///// </summary>
		///// <param name="disposing"></param>
		//private void Dispose(bool disposing)
		//{
		//	// Check to see if Dispose has already been called.
		//	if (!_disposed)
		//	{
		//		// If disposing equals true, dispose all managed 
		//		// and unmanaged resources.
		//		if (disposing)
		//		{
		//			if (_context is null == false)
		//			{
		//				_context.Dispose();
		//			}
		//		}

		//		// release all UNMANAGED system resources A holds.  for
		//		// example, if this class has a Windows handle, let
		//		// it go.
		//		_disposed = true;
		//	}
		//}

		///// <summary>
		///// Define a method, which will only be called in the
		///// DEBUG version of the program, which handles the case
		///// that the programmer neglected to call Dispose() on this
		///// object.  this method could throw an exception indicating
		///// the programmer broke the rules, or could write a trace
		///// string
		///// </summary>
		//[System.Diagnostics.Conditional("DEBUG")]
		//private void DisposeNotCalled()
		//{
		//	// the programmer did NOT call the Dispose() method
		//	// this method will only be called when the destructor
		//	// is invoked by GC.  properly calling Dispose()
		//	// suppresses finalization on this object
		//	// Either trace a message or throw an exception
		//	throw new ApplicationException(String.Format("{0}.Dispose() not called",
		//	   GetType().Name));
		//}

		//#endregion IDisposable Members
	}
}