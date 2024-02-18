using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.CategoryExceptions;
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
            if (viewModel.DateTime < DateTime.UtcNow) throw new InvalidReservatinDateException("DateTime", "mshgjre");
            Reservation reservation = new Reservation()
            {
                FullName = viewModel.FullName,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                DateTime = viewModel.DateTime,
                CreatedDate = DateTime.UtcNow.AddHours(4),
                UpdatedDate= DateTime.UtcNow.AddHours(4),
                IsDeleted=false
            };

            await _repository.CreateAsync(reservation);
            await _repository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var reservation = await _repository.SingleAsync(c => c.Id == id);
            if (reservation == null) throw new ReservationNullException("Entity cannot be null!");
            _repository.Delete(reservation);
            await _repository.CommitAsync();
        }

        public async Task<List<Reservation>> GetAllAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? include)
        {
            return await _repository.GetAllWhere(expression, include).ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? include)
        {
            return await _repository.SingleAsync(expression, include);
        }

        public async Task Update(Reservation reservation)
        {
            var existReservation = await _repository.SingleAsync(x => x.Id == reservation.Id);
            if (reservation == null) throw new ReservationNullException("Entity cannot be null!");
            if (reservation.DateTime < DateTime.UtcNow) throw new InvalidReservatinDateException("DateTime", "Invalid reservation date!");
            existReservation.FullName = reservation.FullName;
            existReservation.Email= reservation.Email;
            existReservation.Phone= reservation.Phone;
            existReservation.DateTime = reservation.DateTime;
            existReservation.UpdatedDate=DateTime.UtcNow.AddHours(4);
            await _repository.CommitAsync();
        }            
    }
}
