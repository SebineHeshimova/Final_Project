

namespace Restaurant.Business.CustomException.AccountExceptions.UserAccountExceptions
{
    public class InvalidUserUsernameException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidUserUsernameException()
        {
        }
        public InvalidUserUsernameException(string? message) : base(message)
        {
        }
        public InvalidUserUsernameException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
