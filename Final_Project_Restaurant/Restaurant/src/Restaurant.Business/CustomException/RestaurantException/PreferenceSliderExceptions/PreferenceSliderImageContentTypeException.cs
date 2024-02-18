using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.PreferenceSliderExceptions
{
    public class PreferenceSliderImageContentTypeException:Exception
    {
        public string PropertyName { get; set; }
        public PreferenceSliderImageContentTypeException()
        {
        }
        public PreferenceSliderImageContentTypeException(string? message) : base(message)
        {
        }
        public PreferenceSliderImageContentTypeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
