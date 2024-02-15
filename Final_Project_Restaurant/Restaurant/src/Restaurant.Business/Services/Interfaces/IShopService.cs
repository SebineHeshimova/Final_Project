

using Restaurant.Business.ViewModels;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IShopService
    {
        Task AddToBasket(int foodId);
        Task<OrderViewModel> CheckoutGet();
        Task CheckoutPost(OrderViewModel viewModel);
    }
}
