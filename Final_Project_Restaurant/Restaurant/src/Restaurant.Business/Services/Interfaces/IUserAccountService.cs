using Restaurant.Business.ViewModels;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task Login(UserLoginViewModel viewModel);
        Task Register(UserRegisterViewModel viewModel);
        Task Logout();
        Task<ProfilViewModel> Profile();
	}
}
