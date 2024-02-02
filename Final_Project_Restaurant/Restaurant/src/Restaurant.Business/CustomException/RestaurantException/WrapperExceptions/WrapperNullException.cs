using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.WrapperExceptions
{
	public class WrapperNullException:Exception
	{
		public string PropertyName { get; set; }
		public WrapperNullException()
		{
		}
		public WrapperNullException(string? message) : base(message)
		{
		}
		public WrapperNullException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
