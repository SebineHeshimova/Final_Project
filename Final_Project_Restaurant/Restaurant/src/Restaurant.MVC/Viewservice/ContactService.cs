using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.ViewService
{
	public class ContactService
	{
		private readonly ISettingService _settingService;

		public ContactService(ISettingService settingService)
		{
			_settingService = settingService;
		}

		public async Task<List<Setting>> GetSetting()
		{
			var setting = await _settingService.GetAllAsync();
			return setting;
		}
	}
}
