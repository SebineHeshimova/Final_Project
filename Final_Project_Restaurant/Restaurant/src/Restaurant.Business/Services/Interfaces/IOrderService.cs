using Restaurant.Business.PaginationModel;
using Restaurant.Core.Entiity;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> Detail(int id);
        Task Accept(int id, string AdminComment);
        Task Reject(int id, string AdminComment);
        Task Pending(int id);
        Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>>? expression = null, params string[]? includes);
        Task<Order> GetByIdAsync(Expression<Func<Order, bool>>? expression = null, params string[]? includes);
        Task<PaginatedList<Order>> GetAllPaginatedAsync(int page, int pageSize, Expression<Func<Order, bool>>? expression = null, params string[]? includes);
    }
}
