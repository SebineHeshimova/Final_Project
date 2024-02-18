using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.PreferenceTextExceptions
{
    public class PreferenceTextNullException:Exception
    {
        public string PropertyName { get; set; }
        public PreferenceTextNullException()
        {
        }
        public PreferenceTextNullException(string? message) : base(message)
        {
        }
        public PreferenceTextNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
