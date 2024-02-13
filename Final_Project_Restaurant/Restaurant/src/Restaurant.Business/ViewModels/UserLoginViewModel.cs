using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
