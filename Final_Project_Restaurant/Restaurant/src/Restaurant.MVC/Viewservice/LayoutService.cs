using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Viewservice
{
    public class LayoutService
    {
        private readonly ISettingService _settingService;

        public LayoutService(ISettingService settingService)
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
