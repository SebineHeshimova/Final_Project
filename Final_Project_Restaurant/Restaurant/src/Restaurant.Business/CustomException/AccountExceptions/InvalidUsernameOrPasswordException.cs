using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AccountExceptions
{
	public class InvalidUsernameOrPasswordException:Exception
	{
		public string PropertyName { get; set; }
		public InvalidUsernameOrPasswordException()
		{
		}
		public InvalidUsernameOrPasswordException(string? message) : base(message)
		{
		}
		public InvalidUsernameOrPasswordException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
