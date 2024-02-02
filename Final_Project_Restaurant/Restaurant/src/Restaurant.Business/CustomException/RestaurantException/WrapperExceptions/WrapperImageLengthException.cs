using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.WrapperExceptions
{
	public class WrapperImageLengthException:Exception
	{
		public string PropertyName { get; set; }
		public WrapperImageLengthException()
		{
		}
		public WrapperImageLengthException(string? message) : base(message)
		{
		}
		public WrapperImageLengthException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
