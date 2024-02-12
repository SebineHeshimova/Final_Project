using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.CustomException.AccountExceptions.UserAccountExceptions
{
    public class InvalidUserEmailException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidUserEmailException()
        {
        }
        public InvalidUserEmailException(string? message) : base(message)
        {
        }
        public InvalidUserEmailException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
