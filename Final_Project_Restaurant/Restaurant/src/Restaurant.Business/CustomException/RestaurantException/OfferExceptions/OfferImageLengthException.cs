using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.OfferExceptions
{
    public class OfferImageLengthException:Exception
    {
        public string PropertyName { get; set; }
        public OfferImageLengthException()
        {
        }
        public OfferImageLengthException(string? message) : base(message)
        {
        }
        public OfferImageLengthException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
