using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.FeedbackExceptions
{
    public class FeedbackNullException:Exception
    {
        public string PropertyName { get; set; }
        public FeedbackNullException()
        {
        }
        public FeedbackNullException(string? message) : base(message)
        {
        }
        public FeedbackNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
