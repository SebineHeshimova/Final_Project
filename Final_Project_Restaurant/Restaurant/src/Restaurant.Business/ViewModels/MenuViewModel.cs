using Restaurant.Core.Entiity;

namespace Restaurant.MVC.ViewModels
{
    public class MenuViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Food> Foods { get; set; }
    }
}
