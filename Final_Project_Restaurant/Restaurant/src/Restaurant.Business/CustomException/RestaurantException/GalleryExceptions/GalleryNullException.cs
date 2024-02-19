using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.GalleryExceptions
{
	public class GalleryNullException:Exception
	{
		public string PropertyName {  get; set; }
		public GalleryNullException()
		{
		}
		public GalleryNullException(string? message) : base(message)
		{
		}
		public GalleryNullException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
