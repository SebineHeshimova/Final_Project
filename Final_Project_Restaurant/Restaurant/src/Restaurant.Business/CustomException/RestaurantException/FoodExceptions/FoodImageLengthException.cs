

namespace Restaurant.Business.CustomException.RestaurantException.FoodExceptions
{
    public class FoodImageLengthException:Exception
    {
        public string PropertyName { get; set; }
        public FoodImageLengthException()
        {
        }
        public FoodImageLengthException(string? message) : base(message)
        {
        }
        public FoodImageLengthException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
