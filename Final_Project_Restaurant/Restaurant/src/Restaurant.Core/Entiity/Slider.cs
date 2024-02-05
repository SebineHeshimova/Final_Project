using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Restaurant.Core.Entiity
{
    public class Slider : BaseEntity
    {
        [StringLength(maximumLength: 200, MinimumLength =3)]
        [Required]
        public string Title {  get; set; }
        [StringLength(maximumLength: 200, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }
        [StringLength(maximumLength:20, MinimumLength = 3)]
        [Required]
        public string RedirectText {  get; set; }
        [StringLength(maximumLength:150)]
        [Required]
        public string RedirectUrl {  get; set; }
       
        [StringLength(maximumLength: 100)]
        public string? ImageUrl {  get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
