using Microsoft.AspNetCore.Identity;
using Restaurant.Business.CustomException.AccountExceptions.UserAccountExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;
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

        public UserAccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task Login(UserLoginViewModel viewModel)
        {
            throw new NotImplementedException();
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
                BirthDate = viewModel.Birthdate
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
    }
}
