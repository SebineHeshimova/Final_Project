using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.ViewModels
{
    public class ProfilViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
