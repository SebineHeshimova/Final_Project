using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
	public class AdminLoginViewModel
	{
		[StringLength(maximumLength: 50, MinimumLength = 3)]
		[Required]
		public string UserName { get; set; }
		[StringLength(maximumLength: 50, MinimumLength = 8)]
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
