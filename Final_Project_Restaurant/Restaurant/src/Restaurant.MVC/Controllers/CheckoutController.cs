using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.MVC.ViewModels;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.MVC.Controllers
{
    public class CheckoutController : Controller
    {

        private readonly IFoodRepository _foodRepository;

        public CheckoutController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToBasket(int foodId)
        {
            if (!_foodRepository.Table.Any(f => f.Id == foodId)) return NotFound();


            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();
            string basketItemsStr = HttpContext.Request.Cookies["BasketItem"];
            BasketItemViewModel basketItem = null;
            if (basketItemsStr is not null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                basketItem = basketItems.FirstOrDefault(b => b.FoodId == foodId);
                if (basketItem != null)
                {
                    basketItem.Count++;
                }
                else
                {
                    basketItem = new BasketItemViewModel()
                    {
                        FoodId = foodId,
                        Count = 1
                    };
                    basketItems.Add(basketItem);
                }

            }
            else
            {
                basketItem = new BasketItemViewModel()
                {
                    FoodId = foodId,
                    Count = 1
                };
                basketItems.Add(basketItem);
            }
            basketItemsStr = JsonConvert.SerializeObject(basketItems);
            HttpContext.Response.Cookies.Append("BasketItem", basketItemsStr);
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
            List<CheckoutViewModel> checkoutVMList = new List<CheckoutViewModel>();
            List<BasketItemViewModel> basketItemVMList=new List<BasketItemViewModel>();
            CheckoutViewModel checkoutVM = null;
            string basketItemVMListStr = HttpContext.Request.Cookies["BasketItem"];
            if(basketItemVMListStr != null)
            {
                basketItemVMList = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemVMListStr);
                foreach(var item in basketItemVMList)
                {
                    checkoutVM = new CheckoutViewModel()
                    {
                        Food = await _foodRepository.SingleAsync(f => f.Id == item.FoodId),
                        Count = item.Count,
                    };
                    checkoutVMList.Add(checkoutVM);
                }
            }
            return View(checkoutVMList);
        }
    }
}
