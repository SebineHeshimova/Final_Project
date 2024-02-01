using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.BannerExceptions
{
    public class BannerImageLengthException:Exception
    {
        public string PropertyName { get; set; }
        public BannerImageLengthException()
        {
        }
        public BannerImageLengthException(string? message) : base(message)
        {
        }
        public BannerImageLengthException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
