using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.SliderException
{
    public class SliderNullException : Exception
    {
        public string PropertyName { get; set; }
        public SliderNullException()
        {
        }
        public SliderNullException(string? message) : base(message)
        {
        }
        public SliderNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
