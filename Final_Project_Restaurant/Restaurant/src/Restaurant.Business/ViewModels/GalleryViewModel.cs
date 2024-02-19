using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
	public class GalleryViewModel
	{
		public List<Slider> Sliders { get; set; }
		public List<Gallery> Galleries { get; set; }
		public List<Video> Videos { get; set; }
	}
}
