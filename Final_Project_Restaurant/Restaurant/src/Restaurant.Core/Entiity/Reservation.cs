using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
    public class Reservation:BaseEntity
    {
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        public string FullName { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 30)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public string? AppUserId {  get; set; }
        public AppUser? User { get; set; }
    }
}
