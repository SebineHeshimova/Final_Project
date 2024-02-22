using Restaurant.Business.CustomException.RestaurantException.AccountExceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
    public class UserRegisterViewModel
    {
        [StringLength(maximumLength:50, MinimumLength =3)]
        [Required]
        public string UserName { get; set; }
		[StringLength(maximumLength: 50, MinimumLength = 3)]
		[Required]
		public string Fullname {  get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public string Birthdate {  get; set; }
		[StringLength(maximumLength: 30, MinimumLength = 8)]
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[StringLength(maximumLength: 30, MinimumLength = 8)]
		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword {  get; set; }
    }
}
