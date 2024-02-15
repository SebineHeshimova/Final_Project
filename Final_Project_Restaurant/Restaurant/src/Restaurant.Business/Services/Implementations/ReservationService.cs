using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.ReservationExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Create(ReservationViewModel viewModel)
        {
            if(viewModel == null) throw new ReservationNullException("Entity cannot be null!");
            if (viewModel.Date < DateTime.Today) throw new InvalidReservatinDateException("Date", "mshgjre");
           // if (viewModel.Time < TimeSpan.) throw new InvalidReservationTimeException("Time", "sjghejjr");
            Reservation reservation = new Reservation()
            {
                FullName = viewModel.FullName,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Date = viewModel.Date,
                Time = viewModel.Time,
                CreatedDate = DateTime.UtcNow.AddHours(4),
                UpdatedDate= DateTime.UtcNow.AddHours(4),
                IsDeleted=false
            };

            await _repository.CreateAsync(reservation);
            await _repository.CommitAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reservation>> GetAllAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? include)
        {
            return await _repository.GetAllWhere(expression, include).ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? include)
        {
            return await _repository.SingleAsync(expression, include);
        }

        public Task Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
