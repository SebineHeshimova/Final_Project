using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.ChefExceptions
{
    public class ChefImageContentTypeException:Exception
    {
        public string PropertyName { get; set; }
        public ChefImageContentTypeException()
        {
        }
        public ChefImageContentTypeException(string? message) : base(message)
        {
        }
        public ChefImageContentTypeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
