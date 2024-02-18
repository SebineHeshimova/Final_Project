using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.ChefExceptions
{
    public class ChefImageLengthException:Exception
    {
        public string PropertyName { get; set; }
        public ChefImageLengthException()
        {
        }
        public ChefImageLengthException(string? message) : base(message)
        {
        }
        public ChefImageLengthException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
