using Restaurant.Business.ViewModels;

namespace Restaurant.Business.Services.Interfaces
{
	public interface IAdminAccountService
	{
		Task Login(AdminLoginViewModel viewModel);
	}
}
