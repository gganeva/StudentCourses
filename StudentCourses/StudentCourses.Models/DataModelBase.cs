using System;
using System.ComponentModel.DataAnnotations;

namespace StudentCourses.Models
{
	/// <summary>
	/// A base class for a data model.
	/// </summary>
	public abstract class DataModelBase : IDeletable, IAuditable
	{
		#region Constructor

		public DataModelBase()
		{
			Id = Guid.NewGuid();
		}

		#endregion // Constructor

		#region IDeletable Members

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }

		#endregion  // IDeletable Members

		#region // IAuditable Members

		[DataType(DataType.DateTime)]
		public DateTime? CreatedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? ModifiedOn { get; set; }

		#endregion // IAuditable Members

		#region Public Members

		[Key]
		public Guid Id { get; set; }

		#endregion  // Public Members
	}   // End class DataModelBase
}   // End namespace StudentCourses.Models