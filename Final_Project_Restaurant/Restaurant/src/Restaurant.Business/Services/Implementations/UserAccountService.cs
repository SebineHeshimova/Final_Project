using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.AccountExceptions.UserAccountExceptions;
using Restaurant.Business.CustomException.RestaurantException.AccountExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
		private readonly IHttpContextAccessor _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IReservationRepository _reservationRepository;
        public UserAccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor context, IOrderRepository orderRepository, IReservationRepository reservationRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _orderRepository = orderRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task Login(UserLoginViewModel viewModel)
        {
            AppUser user = null;
            user = await _userManager.FindByNameAsync(viewModel.UserName);
            if (user == null)
            {
                throw new InvalidUsernameOrPasswordException("", "Invalid UserName or Password!");
            }
            var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);
            if (!result.Succeeded)
            {
                throw new InvalidUsernameOrPasswordException("", "Invalid UserName or Password!");
            }
        }

        public async Task Register(UserRegisterViewModel viewModel)
        {
            AppUser user = null;
            user = await _userManager.FindByNameAsync(viewModel.UserName);
            if (user != null)
            {
                throw new InvalidUserUsernameException("UserName", "Username already exist");
            }
            user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                throw new InvalidUserEmailException("Email", "Email already exist");
            }
            AppUser appUser = new AppUser()
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                FullName = viewModel.Fullname,
                BirthDate = viewModel.Birthdate,
                PhoneNumber=viewModel.Phone,
               
            };
            var result = await _userManager.CreateAsync(appUser, viewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new InvalidUserPasswordException("", error.Description);
                }
            }
            await _userManager.AddToRoleAsync(appUser, "User");
        }
		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}
		public async Task<ProfilViewModel> Profile()
		{
			AppUser appUser = null;

			if (_context.HttpContext.User.Identity.IsAuthenticated)
			{
				appUser = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
			}

			List<Order> orders = await _orderRepository.GetAllWhere(x => x.AppUserId == appUser.Id, "OrderItems").ToListAsync();
            List<Reservation> reservations = await _reservationRepository.GetAllWhere(x => x.AppUserId == appUser.Id).ToListAsync();
            ProfilViewModel viewModel = new ProfilViewModel()
            {
                Orders = orders,
                Reservations = reservations
            };
            return viewModel;
		}

	}
}
