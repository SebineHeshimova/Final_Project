using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.CategoryExceptions;
using Restaurant.Business.CustomException.RestaurantException.PreferenceTextExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class PreferenceTextService : IPreferenceTextService
    {
        private readonly IPreferenceTextRepository _repository;

        public PreferenceTextService(IPreferenceTextRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(PreferenceText preferenceText)
        {
            if (preferenceText == null) throw new PreferenceTextNullException("Entity cannot be null!");
            await _repository.CreateAsync(preferenceText);
            await _repository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var preferenceText = await _repository.SingleAsync(p => p.Id == id);
            if (preferenceText == null) throw new PreferenceTextNullException("Entity cannot be null!");
            _repository.Delete(preferenceText);
            await _repository.CommitAsync();
        }

        public async Task<List<PreferenceText>> GetAllAsync(Expression<Func<PreferenceText, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<PreferenceText> GetByIdAsync(Expression<Func<PreferenceText, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task SoftDelete(int id)
        {
            var preferenceText = await _repository.SingleAsync(p => p.Id == id);
            if (preferenceText == null) throw new PreferenceTextNullException("Entity cannot be null!");
            preferenceText.IsDeleted = !preferenceText.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(PreferenceText preferenceText)
        {
            var existPreferenceText = await _repository.SingleAsync(p => p.Id == preferenceText.Id);
            if (existPreferenceText == null) throw new CategoryNullException("Entity cannot be null!");
            existPreferenceText.Title = preferenceText.Title;
            existPreferenceText.Description = preferenceText.Description;
            existPreferenceText.RedirectText = preferenceText.RedirectText;
            existPreferenceText.RedirectUrl = preferenceText.RedirectUrl;
            await _repository.CommitAsync();
        }
    }
}
