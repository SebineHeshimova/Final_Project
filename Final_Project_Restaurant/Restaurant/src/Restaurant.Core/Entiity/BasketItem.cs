using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
	public class BasketItem:BaseEntity
	{
		public int FoodId {  get; set; }
		public Food Food { get; set; }
		public int Count {  get; set; }
		public string UserId {  get; set; }
		public AppUser User { get; set; }
	}
}
