using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
    public class Food:BaseEntity
    {
        [StringLength(maximumLength:100,MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [Required]
        public string Composition { get; set; }
        [Required]
        public double Price {  get; set; }
        [StringLength(maximumLength:100)]
        public string? ImageUrl { get; set;}
        [NotMapped]
        public IFormFile? ImageFile { get; set;}
        public bool IsNew { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
