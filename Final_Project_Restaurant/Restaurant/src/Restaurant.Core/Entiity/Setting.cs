﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
	public class Setting:BaseEntity
	{
		[StringLength(maximumLength:50, MinimumLength = 3)]
		[Required]
		public string Key {  get; set; }
		[StringLength(maximumLength:100, MinimumLength = 3)]
		[Required]
		public string Value { get; set; }
	}
}
