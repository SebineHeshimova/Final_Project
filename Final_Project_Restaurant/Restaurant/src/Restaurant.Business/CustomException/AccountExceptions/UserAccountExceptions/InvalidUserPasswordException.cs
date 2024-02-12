using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.AccountExceptions.UserAccountExceptions
{
    public class InvalidUserPasswordException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidUserPasswordException()
        {
        }
        public InvalidUserPasswordException(string? message) : base(message)
        {
        }
        public InvalidUserPasswordException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
