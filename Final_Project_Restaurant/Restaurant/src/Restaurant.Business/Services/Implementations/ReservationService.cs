using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<AppUser> _userManager;
        public ReservationService(IReservationRepository repository, IHttpContextAccessor context, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _context = context;
            _userManager = userManager;
        }

      
        public async Task CreatePOST(ReservationViewModel viewModel)
        {

            if (viewModel == null) throw new ReservationNullException("Entity cannot be null!");
            if (viewModel.DateTime < DateTime.Now.AddMinutes(60)) throw new InvalidReservatinDateException("DateTime", "Rezervasiya tarixi yalnız hazırki vaxtdan 1 saat sonraya təyin oluna bilər!");
            AppUser user = null;
            if (_context.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
            }

            Reservation reservation = new Reservation()
            {
                FullName = viewModel.FullName,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                GuestCount = viewModel.GuestCount,
                DateTime = viewModel.DateTime,
                CreatedDate = DateTime.UtcNow.AddHours(4),
                UpdatedDate = DateTime.UtcNow.AddHours(4),
                AppUserId=user.Id,
                IsDeleted = false
            };

            await _repository.CreateAsync(reservation);
            await _repository.CommitAsync();
        }
        public async Task<ReservationViewModel> CreateGet()
        {

            //  if (viewmodel == null) throw new reservationnullexception("reservationviewmodel cannot be null!");
            // 
            AppUser user = null;
            if (_context.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
            }

            ReservationViewModel viewmodel = new ReservationViewModel()
            {
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.PhoneNumber,


            };


            return viewmodel;
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
            
            DateTime date = new DateTime();
            date = reservation.DateTime;
            DateTime newDate=date.AddMinutes(60);
           
            var existReservation = await _repository.SingleAsync(x => x.Id == reservation.Id);
            if (reservation == null) throw new ReservationNullException("Entity cannot be null!");
            if (reservation.DateTime < DateTime.Now.AddMinutes(60) || DateTime.Now.AddMinutes(60) > existReservation.DateTime) throw new InvalidReservatinDateException("DateTime", "Rezervasiya tarixi dəyişdirilə bilməz! Tarix hazırki vaxtdan əvvələ təyin olunub və ya rezervasiya vaxtına 1 saat qalıb!");
            existReservation.FullName = reservation.FullName;
            existReservation.Email = reservation.Email;
            existReservation.Phone = reservation.Phone;
            existReservation.DateTime = reservation.DateTime;
            existReservation.GuestCount = reservation.GuestCount;
            existReservation.UpdatedDate = DateTime.UtcNow.AddHours(4);
            await _repository.CommitAsync();
        }
    }
}
