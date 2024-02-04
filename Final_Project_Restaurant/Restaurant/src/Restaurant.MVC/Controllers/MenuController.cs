using Microsoft.AspNetCore.Mvc;
using Restaurant.MVC.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
