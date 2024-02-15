using Restaurant.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
    public class OrderViewModel
    {

        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        public string FullName { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        public string City { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        public string Address { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public List<CheckoutViewModel>? CheckoutViewModel { get; set; }
    }
}
