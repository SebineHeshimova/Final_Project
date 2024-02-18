using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IChefService
    {
        Task CreateAsync(Chef chef);
        Task UpdateAsync(Chef chef);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<Chef>> GetAllAsync(Expression<Func<Chef, bool>>? expression = null, params string[]? includes);
        Task<Chef> GetByIdAsync(Expression<Func<Chef, bool>>? expression = null, params string[]? includes);
    }
}
