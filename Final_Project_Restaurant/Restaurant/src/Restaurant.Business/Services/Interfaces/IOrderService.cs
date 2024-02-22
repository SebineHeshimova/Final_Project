using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> Detail(int id);
        Task Accept(int id);
        Task Reject(int id);
        Task Pending(int id);
        Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>>? expression = null, params string[]? includes);
        Task<Order> GetByIdAsync(Expression<Func<Order, bool>>? expression = null, params string[]? includes);
    }
}
