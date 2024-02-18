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
    public class Chef: BaseEntity
    {
        [StringLength(maximumLength: 100, MinimumLength =3)]
        [Required]
        public string FullName {  get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        [Required]
        public string Position {  get; set; }
        [StringLength(maximumLength:100)]
        public string? ImageUrl {  get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
            }
}
