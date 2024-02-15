using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.ReservationExceptions
{
    public class InvalidReservationTimeException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidReservationTimeException()
        {
        }
        public InvalidReservationTimeException(string? message) : base(message)
        {
        }
        public InvalidReservationTimeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
