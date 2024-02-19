using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
	public class Video:BaseEntity
	{
		[StringLength(maximumLength: 100)]
		public string? ImageUrl { get; set; }
		[NotMapped]
		public IFormFile? ImageFile { get; set; }
		[StringLength(maximumLength: 200)]
		[Required]
		public string RedirectUrl {  get; set; }
	}
}
