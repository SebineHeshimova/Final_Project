using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
    public class ReservationViewModel
    {
        [StringLength(maximumLength: 50, MinimumLength =3)]
        [Required]
        public string FullName { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength:30)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public List<Slider> Sliders { get; set; }
    }
}
