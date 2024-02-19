using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
	public class Gallery:BaseEntity
	{
		[StringLength(maximumLength:100)]
		public string? ImageUrl {  get; set; }
		[NotMapped]
		public IFormFile? ImageFile { get; set; }	
	}
}
