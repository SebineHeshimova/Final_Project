using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant.Business.CustomException.RestaurantException.FoodExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Data.Repositories.Implementations;
using Restaurant.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class ShopService : IShopService
    {
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFoodRepository _foodRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IOrderRepository _orderRepository;
        public ShopService(UserManager<AppUser> userManager, IFoodRepository foodRepository, IBasketItemRepository basketItemRepository, IHttpContextAccessor context, IOrderRepository orderRepository)
        {

            _userManager = userManager;
            _foodRepository = foodRepository;
            _basketItemRepository = basketItemRepository;
            _context = context;
            _orderRepository = orderRepository;
        }

        public async Task AddToBasket(int foodId)
        {
            if (!_foodRepository.Table.Any(f => f.Id == foodId)) throw new FoodNullException("Entity cannot be null!");


            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();
            BasketItemViewModel basketItemVM = null;
            BasketItem basketItem = null;
            AppUser user = null;
            if (_context.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
            }
            if (user == null)
            {
                string basketItemsStr = _context.HttpContext.Request.Cookies["BasketItem"];
                if (basketItemsStr is not null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                    basketItemVM = basketItems.FirstOrDefault(b => b.FoodId == foodId);
                    if (basketItemVM != null)
                    {
                        basketItemVM.Count++;
                    }
                    else
                    {
                        basketItemVM = new BasketItemViewModel()
                        {
                            FoodId = foodId,
                            Count = 1
                        };
                        basketItems.Add(basketItemVM);
                    }

                }
                else
                {
                    basketItemVM = new BasketItemViewModel()
                    {
                        FoodId = foodId,
                        Count = 1
                    };
                    basketItems.Add(basketItemVM);
                }
                basketItemsStr = JsonConvert.SerializeObject(basketItems);
                _context.HttpContext.Response.Cookies.Append("BasketItem", basketItemsStr);
            }
            else
            {
                basketItem = await _basketItemRepository.Table.FirstOrDefaultAsync(b => b.FoodId == foodId && b.UserId == user.Id && b.IsDeleted==false);
                if (basketItem != null)
                {
                    basketItem.Count++;
                    basketItem.UpdatedDate = DateTime.UtcNow.AddHours(4);
                }
                else
                {
                    basketItem = new BasketItem()
                    {
                        FoodId = foodId,
                        Count = 1,
                        UserId = user.Id,
                        IsDeleted=false,
                        CreatedDate=DateTime.UtcNow.AddHours(4),
                        UpdatedDate=DateTime.UtcNow.AddHours(4),
                    };
                    await _basketItemRepository.CreateAsync(basketItem);
                }
                await _basketItemRepository.CommitAsync();
            }
        }

        public async Task<OrderViewModel> CheckoutGet()
        {
            List<CheckoutViewModel> checkoutVMList = new List<CheckoutViewModel>();
            List<BasketItemViewModel> basketItemVMList = new List<BasketItemViewModel>();
            List<BasketItem> basketItems = new List<BasketItem>();
            CheckoutViewModel checkoutVM = null;
            AppUser user = null;
            if (_context.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
            }
            if (user == null)
            {
                string basketItemVMListStr = _context.HttpContext.Request.Cookies["BasketItem"];
                if (basketItemVMListStr != null)
                {
                    basketItemVMList = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemVMListStr);
                    foreach (var item in basketItemVMList)
                    {
                        checkoutVM = new CheckoutViewModel()
                        {
                            Food = await _foodRepository.SingleAsync(f => f.Id == item.FoodId),
                            Count = item.Count,
                        };
                        checkoutVMList.Add(checkoutVM);
                    }
                }
            }
            else
            {
                basketItems = await _basketItemRepository.Table.Include(b=>b.Food).Where(b => b.UserId == user.Id && b.IsDeleted==false).ToListAsync();
                foreach (var item in basketItems)
                {
                    checkoutVM = new CheckoutViewModel()
                    {
                        Food = await _foodRepository.SingleAsync(f => f.Id == item.FoodId),
                        Count = item.Count,
                    };
                    checkoutVMList.Add(checkoutVM);
                }
            }
            
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                CheckoutViewModels = checkoutVMList,
                FullName=user?.FullName,
                Email=user?.Email,
                Phone=user?.PhoneNumber
                
            };
            
            if(orderViewModel == null)
            {
                throw new NullReferenceException();
            }
            return orderViewModel;
        }

        public async Task CheckoutPost(OrderViewModel viewModel)
        {

            List<CheckoutViewModel> checkoutItemList = new List<CheckoutViewModel>();
            List<BasketItemViewModel> basketItemList = new List<BasketItemViewModel>();
            List<BasketItem> basketItems = new List<BasketItem>();
            double delivery = 5;
            double sum=0;
            OrderItem orderItem = null;
            AppUser user = null;
            if (_context.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_context.HttpContext.User.Identity.Name);
            }
            Order order = new Order()
            {
                FullName = viewModel.FullName,
                Email = viewModel.Email,
                City = viewModel.City,
                Address = viewModel.Address,
                Phone = viewModel.Phone,
                AppUserId = user?.Id,
                OrderItems = new List<OrderItem>(),
                CreatedDate = DateTime.UtcNow.AddHours(4),
                UpdatedDate = DateTime.UtcNow.AddHours(4),
                
            };

            if (user == null)
            {
                string basketViewstr =_context.HttpContext.Request.Cookies["BasketItem"];
                if (basketViewstr != null)
                {
                    basketItemList = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketViewstr);
                    foreach (var item in basketItemList)
                    {
                        Food food = await _foodRepository.SingleAsync(x => x.Id == item.FoodId);
                        orderItem = new OrderItem
                        {
                            Food = food,
                            FoodName = food.Name,
                            Price = food.Price,
                            Count = item.Count,
                            Order = order,
                            
                        };
                        sum += (orderItem.Price * orderItem.Count);
                        order.TotalPrice =sum + delivery;
                        order.OrderItems.Add(orderItem);
                    }
                }
            }
            else
            {
                basketItems = await _basketItemRepository.Table.Include(x => x.Food).Where(x => x.UserId == user.Id && !x.IsDeleted).ToListAsync();
                foreach (var item in basketItems)
                {
                    Food food = await _foodRepository.SingleAsync(x => x.Id == item.FoodId);
                    
                    orderItem = new OrderItem
                    {
                        Food = food,
                        FoodName = food.Name,
                        Price = food.Price,
                        Count = item.Count,
                        Order = order
                    };
                    sum += (orderItem.Price * orderItem.Count);
                    order.TotalPrice = sum + delivery;
                    order.OrderItems.Add(orderItem);
                    item.IsDeleted = true;
                }
            }
            await _orderRepository.Table.AddAsync(order);
            await _orderRepository.CommitAsync();
        }
    }
}
