using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.ReservationExceptions
{
    public class ReservationAppUserException:Exception
    {
        public string PropertyName { get; set; }
        public ReservationAppUserException()
        {
        }
        public ReservationAppUserException(string? message) : base(message)
        {
        }
        public ReservationAppUserException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
