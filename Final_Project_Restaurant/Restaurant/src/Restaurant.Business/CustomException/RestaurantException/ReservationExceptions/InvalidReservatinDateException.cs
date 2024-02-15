using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.ReservationExceptions
{
    public class InvalidReservatinDateException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidReservatinDateException()
        {
        }
        public InvalidReservatinDateException(string? message) : base(message)
        {
        }
        public InvalidReservatinDateException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
