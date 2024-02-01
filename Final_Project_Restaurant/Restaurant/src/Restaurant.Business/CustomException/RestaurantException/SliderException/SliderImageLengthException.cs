using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.SliderException
{
    public class SliderImageLengthException:Exception
    {
        public string PropertyName { get; set; }
        public SliderImageLengthException()
        {
        }
        public SliderImageLengthException(string? message) : base(message)
        {
        }
        public SliderImageLengthException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
