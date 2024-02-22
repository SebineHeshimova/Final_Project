using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.OrderExceptions
{
    public class OrderNullException:Exception
    {
        public string PropertyName { get; set; }
        public OrderNullException()
        {
        }
        public OrderNullException(string? message) : base(message)
        {
        }
        public OrderNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
