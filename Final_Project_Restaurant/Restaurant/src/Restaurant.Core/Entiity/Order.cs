using Restaurant.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
    public class Order:BaseEntity
    {
        [StringLength(maximumLength:50,MinimumLength =3)]
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
        public double TotalPrice { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus Status { get; set; }
    }
}
