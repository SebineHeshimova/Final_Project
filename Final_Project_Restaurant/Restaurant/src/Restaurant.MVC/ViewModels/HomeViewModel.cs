using Restaurant.Core.Entiity;

namespace Restaurant.MVC.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Banner> Banners { get; set; }
        public Wrapper Wrappers { get; set; }
    }
}
