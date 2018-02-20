using StudentCourses.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentCourses.Models
{
	public abstract class DataModelBase : IDeletable, IAuditable
	{
		public DataModelBase()
		{
			Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		public bool IsDeleted { get; set; }
		
		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? CreatedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? ModifiedOn { get; set; }
	}
}