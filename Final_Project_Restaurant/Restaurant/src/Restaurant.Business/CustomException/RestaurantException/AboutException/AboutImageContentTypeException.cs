using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AboutException
{
	public class AboutImageContentTypeException:Exception
	{
		public string PropertyName { get; set; }
		public AboutImageContentTypeException()
		{
		}
		public AboutImageContentTypeException(string? message) : base(message)
		{
		}
		public AboutImageContentTypeException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
