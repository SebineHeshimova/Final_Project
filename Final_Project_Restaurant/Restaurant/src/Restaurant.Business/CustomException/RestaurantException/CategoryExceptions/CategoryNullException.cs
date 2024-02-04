

namespace Restaurant.Business.CustomException.RestaurantException.CategoryExceptions
{
    public class CategoryNullException:Exception
    {
        public string PropertyName { get; set; }
        public CategoryNullException()
        {
        }
        public CategoryNullException(string? message) : base(message)
        {
        }
        public CategoryNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
