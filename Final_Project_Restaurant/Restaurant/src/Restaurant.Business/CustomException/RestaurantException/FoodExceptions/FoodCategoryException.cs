using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.FoodExceptions
{
    public class FoodCategoryException:Exception
    {
        public string PropertyName { get; set; }
        public FoodCategoryException()
        {
        }
        public FoodCategoryException(string? message) : base(message)
        {
        }
        public FoodCategoryException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
