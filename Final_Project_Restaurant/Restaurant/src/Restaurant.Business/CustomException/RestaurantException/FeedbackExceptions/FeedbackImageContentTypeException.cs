using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.FeedbackExceptions
{
    public class FeedbackImageContentTypeException:Exception
    {
        public string PropertyName { get; set; }
        public FeedbackImageContentTypeException()
        {
        }
        public FeedbackImageContentTypeException(string? message) : base(message)
        {
        }
        public FeedbackImageContentTypeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
