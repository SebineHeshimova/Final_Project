using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.GalleryExceptions
{
	public class GalleryImageContentTypeException:Exception
	{
		public string PropertyName { get; set; }
		public GalleryImageContentTypeException()
		{
		}
		public GalleryImageContentTypeException(string? message) : base(message)
		{
		}
		public GalleryImageContentTypeException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
