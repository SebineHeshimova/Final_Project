using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.OfferExceptions
{
    public class OfferNullException:Exception
    {
        public string PropertyName { get; set; }
        public OfferNullException()
        {
        }
        public OfferNullException(string? message) : base(message)
        {
        }
        public OfferNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
