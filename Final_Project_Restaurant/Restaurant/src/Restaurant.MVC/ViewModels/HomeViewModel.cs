using Restaurant.Core.Entiity;

namespace Restaurant.MVC.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Wrapper> Wrappers { get; set; }
        public List<About> Abouts { get; set; }
        public List<Offer> Offers { get; set; }
    }
}
