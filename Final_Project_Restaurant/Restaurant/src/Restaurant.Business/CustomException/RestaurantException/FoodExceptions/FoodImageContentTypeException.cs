

namespace Restaurant.Business.CustomException.RestaurantException.FoodExceptions
{
    public class FoodImageContentTypeException:Exception
    {
        public string PropertyName { get; set; }
        public FoodImageContentTypeException()
        {
        }
        public FoodImageContentTypeException(string? message) : base(message)
        {
        }
        public FoodImageContentTypeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
