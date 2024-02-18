using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AboutException
{
	public class AboutSignatureImageNullException:Exception
	{
		public string PropertyName { get; set; }
		public AboutSignatureImageNullException()
		{
		}
		public AboutSignatureImageNullException(string? message) : base(message)
		{
		}
		public AboutSignatureImageNullException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
