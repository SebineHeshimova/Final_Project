using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Entiity
{
    public class OrderItem:BaseEntity
    {
        [StringLength(maximumLength:50)]
        [Required]
        public string FoodName { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        public double Price {  get; set; }
    }
}
