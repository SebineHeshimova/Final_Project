﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
	public class About: BaseEntity
	{
		[StringLength(maximumLength: 100, MinimumLength = 3)]
		[Required]
		public string Title {  get; set; }
		[StringLength(maximumLength: 1000, MinimumLength = 3)]
		[Required]
		public string Description { get; set; }
		[StringLength(maximumLength: 200)]
		[Required]
		public string RedirectUrl {  get; set; }
		[StringLength(maximumLength: 100)]
		public string? ImageUrl {  get; set; }
		[NotMapped]
		public IFormFile? ImageFile { get; set; }
		[StringLength(maximumLength: 100)]
		public string? SignatureImageUrl { get; set; }
		[NotMapped]
		public IFormFile? SignatureImageFile { get; set; }
	}
}
