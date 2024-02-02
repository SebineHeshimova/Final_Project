using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AboutException
{
	public class AboutImageLengthException:Exception
	{
		public string PropertyName { get; set; }
		public AboutImageLengthException()
		{
		}
		public AboutImageLengthException(string? message) : base(message)
		{
		}
		public AboutImageLengthException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
