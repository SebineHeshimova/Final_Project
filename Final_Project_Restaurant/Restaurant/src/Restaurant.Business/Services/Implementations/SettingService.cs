using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.SettingException;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Implementations
{
	public class SettingService : ISettingService
	{
		private readonly ISettingRepository _settingRepository;

		public SettingService(ISettingRepository settingRepository)
		{
			_settingRepository = settingRepository;
		}

		public async Task<List<Setting>> GetAllAsync(Expression<Func<Setting, bool>>? expression = null, params string[]? includes)
		{
			return await _settingRepository.GetAllWhere(expression, includes).ToListAsync();
		}

		public async Task<Setting> GetByIdAsync(Expression<Func<Setting, bool>>? expression = null, params string[]? includes)
		{
			return await _settingRepository.SingleAsync(expression, includes);
		}


		public async Task UpdateAsync(Setting setting)
		{
			var existSetting = await _settingRepository.SingleAsync(s => s.Id == setting.Id);
			if (existSetting == null) throw new SettingNullException("Entity can not null!");
			existSetting.UpdatedDate = DateTime.UtcNow.AddHours(4);
			existSetting.Value = setting.Value;
			await _settingRepository.CommitAsync();
		}
	}
}
