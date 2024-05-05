﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.RestaurantException.ReservationExceptions
{
    public class ReservationAdminCommentNullException:Exception
    {
        public string PropertyName { get; set; }
        public ReservationAdminCommentNullException()
        {
        }
        public ReservationAdminCommentNullException(string? message) : base(message)
        {
        }
        public ReservationAdminCommentNullException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
