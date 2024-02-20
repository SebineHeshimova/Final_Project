using Microsoft.AspNetCore.Identity;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.ViewService
{
    public class LayoutService
    {
        private readonly ISettingService _settingService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LayoutService(ISettingService settingService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
		{
			_settingService = settingService;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public async Task<List<Setting>> GetSetting()
        {
            var setting = await _settingService.GetAllAsync();
            return setting;
        }
		public async Task<AppUser> GetAppUser()
		{
			AppUser user = null;
			string userName = null;
			if (_httpContextAccessor.HttpContext.User.Identity.Name != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
			{
				userName = _httpContextAccessor.HttpContext.User.Identity.Name;
			}
			if (userName != null)
			{
				user = await _userManager.FindByNameAsync(userName);
			}


			return user;
		}
	}
}
