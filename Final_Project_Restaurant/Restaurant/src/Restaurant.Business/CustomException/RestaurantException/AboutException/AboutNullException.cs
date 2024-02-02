using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AboutException
{
	public class AboutNullException:Exception
	{
		public string PropertyName { get; set; }
		public AboutNullException()
		{
		}
		public AboutNullException(string? message) : base(message)
		{
		}
		public AboutNullException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
