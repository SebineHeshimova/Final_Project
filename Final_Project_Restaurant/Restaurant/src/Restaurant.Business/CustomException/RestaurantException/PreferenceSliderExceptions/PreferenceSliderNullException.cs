using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.PreferenceSliderExceptions
{
    public class PreferenceSliderNullException:Exception
    {
        public string PropertyName { get; set; }
        public PreferenceSliderNullException()
        {
        }
        public PreferenceSliderNullException(string? message) : base(message)
        {
        }
        public PreferenceSliderNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
