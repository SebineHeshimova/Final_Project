

namespace Restaurant.Business.CustomException.RestaurantException.FoodExceptions
{
    public class FoodNullException:Exception
    {
        public string PropertyName { get; set; }
        public FoodNullException()
        {
        }
        public FoodNullException(string? message) : base(message)
        {
        }
        public FoodNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
