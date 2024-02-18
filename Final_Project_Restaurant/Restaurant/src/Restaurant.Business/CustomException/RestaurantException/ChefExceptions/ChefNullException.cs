using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.ChefExceptions
{
    public class ChefNullException:Exception
    {
        public string PropertyName { get; set; }
        public ChefNullException()
        {
        }
        public ChefNullException(string? message) : base(message)
        {
        }
        public ChefNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
