using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.ReservationExceptions;
using Restaurant.Business.PaginationModel;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Enums;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class ReservationCheckService : IReservationCheckService
    {
        private readonly IReservationRepository _repository;

        public ReservationCheckService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task Accept(int id)
        {
            var reservation = await _repository.SingleAsync(r => r.Id == id);
            if (reservation == null) throw new ReservationNullException("Entity cannot be null!");
            reservation.Status = ReservationStatus.Accepted;
            await _repository.CommitAsync();
        }

        public async Task<Reservation> Detail(int id)
        {
            var reservation = await _repository.SingleAsync(r => r.Id == id);
            if (reservation == null) throw new ReservationNullException("Entity cannot be null!");
            return reservation;
        }

        public async Task<List<Reservation>> GetAllAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<PaginatedList<Reservation>> GetAllPaginatedAsync(int page, int pageSize, Expression<Func<Reservation, bool>>? expression = null, params string[]? includes)
        {
            var query = _repository.GetAllWhere(expression, includes);
            var paginatedList = PaginatedList<Reservation>.Create(query, page, pageSize);
            return paginatedList;
        }

        public async Task<Reservation> GetByIdAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task Pending(int id)
        {
            var reservation = await _repository.SingleAsync(r => r.Id == id);
            if (reservation == null) throw new ReservationNullException("Entity cannot be null!");
            reservation.Status = ReservationStatus.Pending;
            await _repository.CommitAsync();
        }

        public async Task Reject(int id)
        {
            var reservation = await _repository.SingleAsync(r => r.Id == id);
            if (reservation == null) throw new ReservationNullException("Entity cannot be null!");
            reservation.Status = ReservationStatus.Rejected;
            await _repository.CommitAsync();
        }
    }
}
