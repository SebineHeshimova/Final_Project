using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
	public class AppUser:IdentityUser
	{
		[StringLength(maximumLength: 50, MinimumLength =3)]
		[Required]
		public string FullName {  get; set; }
		[Required]
		public string BirthDate {  get; set; }
		public List<BasketItem> BasketItems { get; set; }
		public List<Reservation> Reservations { get; set; }
	}
}
