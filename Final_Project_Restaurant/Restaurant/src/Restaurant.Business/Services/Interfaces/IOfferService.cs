using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IOfferService
    {
        Task CreateAsync(Offer offer);
        Task UpdateAsync(Offer offer);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<Offer>> GetAllAsync(Expression<Func<Offer, bool>>? expression = null, params string[]? includes);
        Task<Offer> GetByIdAsync(Expression<Func<Offer, bool>>? expression = null, params string[]? includes);
    }
}
