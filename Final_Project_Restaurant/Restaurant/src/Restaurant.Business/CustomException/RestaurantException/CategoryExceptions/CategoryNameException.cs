using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.CategoryExceptions
{
    public class CategoryNameException:Exception
    {
        public string PropertyName { get; set; }
        public CategoryNameException()
        {
        }
        public CategoryNameException(string? message) : base(message)
        {
        }
        public CategoryNameException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
