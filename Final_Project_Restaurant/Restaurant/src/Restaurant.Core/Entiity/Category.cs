using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
    public class Category:BaseEntity
    {
		[StringLength(maximumLength: 50, MinimumLength = 3)]
		[Required]
		public string Name { get; set; }
    }
}
