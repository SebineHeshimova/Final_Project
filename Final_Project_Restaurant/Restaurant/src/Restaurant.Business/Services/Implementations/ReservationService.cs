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

        public async Task<ReservationViewModel> CreateGet()
        {
            AppUser appUser = null;
            if (_context.HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
            }
            if(appUser == null) throw new ReservationAppUserException("Appuser null!");
            ReservationViewModel viewModel = new ReservationViewModel()
            {
                FullName = appUser?.FullName,
                Email = appUser?.Email,

            };
            return viewModel;
        }
        public async Task Create(ReservationViewModel viewModel)
        {

            if (viewModel == null) throw new ReservationNullException("Entity cannot be null!");
            if (viewModel.DateTime < DateTime.UtcNow) throw new InvalidReservatinDateException("DateTime", "mshgjre");
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
        //public async Task<ReservationViewModel> CreateGet()
        //{

        //  //  if (viewModel == null) throw new ReservationNullException("ReservationViewModel cannot be null!");
        //  // 
        //    AppUser user = null;
        //    if (_context.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        user = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
        //    }

        //    ReservationViewModel viewModel = new ReservationViewModel()
        //    {
        //        FullName = user.FullName,
        //        Email = user.Email,
        //        Phone = user.PhoneNumber,


        //    };


        //    return viewModel;
        //}
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
            existReservation.Email = reservation.Email;
            existReservation.Phone = reservation.Phone;
            existReservation.DateTime = reservation.DateTime;
            existReservation.GuestCount = reservation.GuestCount;
            existReservation.UpdatedDate = DateTime.UtcNow.AddHours(4);
            await _repository.CommitAsync();
        }
    }
}
