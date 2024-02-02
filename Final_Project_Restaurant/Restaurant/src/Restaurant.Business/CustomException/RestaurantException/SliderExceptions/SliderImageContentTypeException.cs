using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.SliderException
{
    public class SliderImageContentTypeException:Exception
    {
        public string PropertyName { get; set; }
        public SliderImageContentTypeException()
        {
        }
        public SliderImageContentTypeException(string? message) : base(message)
        {
        }
        public SliderImageContentTypeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
