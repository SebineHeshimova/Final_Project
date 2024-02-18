using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.FeedbackExceptions
{
    public class FeedbackImageLengthException:Exception
    {
        public string PropertyName { get; set; }
        public FeedbackImageLengthException()
        {
        }
        public FeedbackImageLengthException(string? message) : base(message)
        {
        }
        public FeedbackImageLengthException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
