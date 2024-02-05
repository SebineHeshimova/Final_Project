using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
	public class Wrapper:BaseEntity
	{
		[StringLength(maximumLength:200, MinimumLength =3)]
		[Required]
		public string Title {  get; set; }
		[StringLength(maximumLength: 200, MinimumLength = 3)]
		[Required]
		public string Description { get; set; }
		[StringLength(maximumLength: 250, MinimumLength = 3)]
		[Required]
		public string SubDescription { get; set; }
		[StringLength(maximumLength: 150)]
		[Required]
		public string RedirectUrl { get; set; }
		[StringLength(maximumLength: 30, MinimumLength = 3)]
		[Required]
		public string RedirectText {  get; set; }
		[StringLength(maximumLength: 100)]
		public string? ImageUrl {  get; set; }
		[NotMapped]
		public IFormFile? ImageFile {  get; set; }
	}
}
