

namespace Restaurant.Business.CustomException.RestaurantException.SettingException
{
	public class SettingNullException:Exception
	{
		public string PropertyName { get; set; }
		public SettingNullException()
		{
		}
		public SettingNullException(string? message) : base(message)
		{
		}
		public SettingNullException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
