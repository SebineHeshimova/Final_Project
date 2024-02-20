using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task Login(UserLoginViewModel viewModel);
        Task Register(UserRegisterViewModel viewModel);
        Task Logout();
        Task<List<Order>> Profile();
	}
}
