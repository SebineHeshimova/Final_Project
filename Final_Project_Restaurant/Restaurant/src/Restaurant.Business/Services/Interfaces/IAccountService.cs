using Restaurant.Business.ViewModels;

namespace Restaurant.Business.Services.Interfaces
{
	public interface IAccountService
	{
		Task Login(AdminLoginViewModel viewModel);
	}
}
