using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.OrderExceptions;
using Restaurant.Business.PaginationModel;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;
using Restaurant.Core.Enums;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Accept(int id)
        {
            var order =await   _repository.SingleAsync(o => o.Id == id);
            if (order == null) throw new OrderNullException("Entity cannot be null!");
            order.Status = OrderStatus.Accepted;
            await _repository.CommitAsync();
        }

        public async Task<Order> Detail(int id)
        {

            var order =await _repository.SingleAsync(o => o.Id == id, "OrderItems");
            if (order == null) throw new OrderNullException("Entity cannot be null!");
            return order;
        }

        public async Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<PaginatedList<Order>> GetAllPaginatedAsync(int page, int pageSize, Expression<Func<Order, bool>>? expression = null, params string[]? includes)
        {
            var query = _repository.GetAllWhere(expression, includes);
            var paginatedList = PaginatedList<Order>.Create(query, page, pageSize);
            return paginatedList;
        }

        public async Task<Order> GetByIdAsync(Expression<Func<Order, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task Pending(int id)
        {
            var order = await _repository.SingleAsync(o => o.Id == id);
            if (order == null) throw new OrderNullException("Entity cannot be null!");
            order.Status = OrderStatus.Pending;
            await _repository.CommitAsync();
        }

        public async Task Reject(int id)
        {
            var order = await _repository.SingleAsync(o => o.Id == id);
            if (order == null) throw new OrderNullException("Entity cannot be null!");
            order.Status = OrderStatus.Rejected;
            await _repository.CommitAsync();
        }
    }
}
