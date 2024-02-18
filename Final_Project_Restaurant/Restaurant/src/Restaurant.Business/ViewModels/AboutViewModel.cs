using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
	public class AboutViewModel
	{
		public List<Slider> Sliders { get; set; }
		public List<About> Abouts { get; set; }
		public List<Chef> Chefs { get; set; }
		public List<Feedback> Feedbacks { get; set; }
	}
}
