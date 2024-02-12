﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AccountExceptions
{
	public class InvalidUsernameAndPasswordException:Exception
	{
		public string PropertyName { get; set; }
		public InvalidUsernameAndPasswordException()
		{
		}
		public InvalidUsernameAndPasswordException(string? message) : base(message)
		{
		}
		public InvalidUsernameAndPasswordException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
