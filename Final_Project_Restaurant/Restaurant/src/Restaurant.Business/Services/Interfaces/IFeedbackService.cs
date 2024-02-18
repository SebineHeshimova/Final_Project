using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task CreateAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<Feedback>> GetAllAsync(Expression<Func<Feedback, bool>>? expression = null, params string[]? includes);
        Task<Feedback> GetByIdAsync(Expression<Func<Feedback, bool>>? expression = null, params string[]? includes);
    }
}
