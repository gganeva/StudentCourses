﻿using System;

namespace StudentCourses.Models
{
	public interface IAuditable
	{
		DateTime? CreatedOn { get; set; }
		DateTime? ModifiedOn { get; set; }
	}
}