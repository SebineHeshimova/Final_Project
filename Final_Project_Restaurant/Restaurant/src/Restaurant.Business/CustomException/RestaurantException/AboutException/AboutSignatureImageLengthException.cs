using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.AboutException
{
	public class AboutSignatureImageLengthException:Exception
	{
		public string PropertyName { get; set; }
		public AboutSignatureImageLengthException()
		{
		}
		public AboutSignatureImageLengthException(string? message) : base(message)
		{
		}
		public AboutSignatureImageLengthException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
