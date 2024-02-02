using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.WrapperExceptions
{
	public class WrapperImageContentTypeException:Exception
	{
		public string PropertyName { get; set; }
		public WrapperImageContentTypeException()
		{
		}
		public WrapperImageContentTypeException(string? message) : base(message)
		{
		}
		public WrapperImageContentTypeException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
