using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.OrderExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Manage")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            var datas = await _orderService.GetAllPaginatedAsync(page, 10);
            return View(datas);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.Detail(id);
            return View(order);
        }
        public async Task<IActionResult> Accept(int id)
        {
            try
            {
                await _orderService.Accept(id); 
            }
            catch(OrderNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(Exception ex) { }
            return RedirectToAction("index", "Order");
        }
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                await _orderService.Reject(id);
            }
            catch (OrderNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("index", "Order");
        }
        public async Task<IActionResult> Pending(int id)
        {
            try
            {
                await _orderService.Pending(id);
            }
            catch (OrderNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("index", "Order");
        }
    }
}
