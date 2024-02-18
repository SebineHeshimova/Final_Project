using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AboutException
{
	public class AboutSignatureImageContentTypeException:Exception
	{
		public string PropertyName { get; set; }
		public AboutSignatureImageContentTypeException()
		{
		}
		public AboutSignatureImageContentTypeException(string? message) : base(message)
		{
		}
		public AboutSignatureImageContentTypeException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
