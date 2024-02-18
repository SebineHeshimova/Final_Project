using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Entiity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.ViewModels;
using Restaurant.MVC.ViewModels;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.CustomException.RestaurantException.FoodExceptions;

namespace Restaurant.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IFoodRepository _foodRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IOrderRepository _orderRepository;

        public ShopController(IFoodRepository foodRepository, UserManager<AppUser> userManager, IBasketItemRepository basketItemRepository, IShopService shopService, IOrderRepository orderRepository)
        {
            _foodRepository = foodRepository;
            _userManager = userManager;
            _basketItemRepository = basketItemRepository;
            _shopService = shopService;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddToBasket(int foodId)
        {
            try
            {
                await _shopService.AddToBasket(foodId);
            }
            catch(FoodNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(Exception ex) { }
            return Ok();
        }

        public IActionResult GetBasketItem()
        {
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();
            string basketItemsStr = HttpContext.Request.Cookies["BasketItem"];
            if (basketItemsStr != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
            }
            return Json(basketItems);
        }
        public async Task<IActionResult> Checkout()
        {
            if(!ModelState.IsValid) return View();
            
            var orderViewModel =  await _shopService.CheckoutGet();
            if (orderViewModel == null) return View();
            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Checkout(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _shopService.CheckoutPost(viewModel);
            }
            catch(NullReferenceException ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("index", "Home");
        }
    }
}
