using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.BannerExceptions
{
    public class BannerImageContentTypeException:Exception
    {
        public string PropertyName { get; set; }
        public BannerImageContentTypeException()
        {
        }
        public BannerImageContentTypeException(string? message) : base(message)
        {
        }
        public BannerImageContentTypeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
