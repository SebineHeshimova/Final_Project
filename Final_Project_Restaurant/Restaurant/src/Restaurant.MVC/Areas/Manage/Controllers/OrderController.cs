using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Accept(int id, string adminComment)
        {
            try
            {
                await _orderService.Accept(id, adminComment); 
            }
            catch(OrderNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(Exception ex) { }
            return RedirectToAction("index", "Order");
        }
        public async Task<IActionResult> Reject(int id, string AdminComment)
        {
            Order order;
            try
            {
                await _orderService.Reject(id, AdminComment);
            }
            catch (OrderNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(OrderAdminCommentNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View("detail",order =await _orderService.GetByIdAsync(o => o.Id == id));
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
