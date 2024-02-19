using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.VideoExceptions
{
	public class VideoImageLengthException:Exception
	{
		public string PropertyName { get; set; }
		public VideoImageLengthException()
		{
		}
		public VideoImageLengthException(string? message) : base(message)
		{
		}
		public VideoImageLengthException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
