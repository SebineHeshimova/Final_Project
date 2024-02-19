using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.VideoExceptions
{
	public class VideoImageContentTypeException:Exception
	{
		public string PropertyName { get; set; }
		public VideoImageContentTypeException()
		{
		}
		public VideoImageContentTypeException(string? message) : base(message)
		{
		}
		public VideoImageContentTypeException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
