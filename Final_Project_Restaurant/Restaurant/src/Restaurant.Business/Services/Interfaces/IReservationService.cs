using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationViewModel> CreateGet();
        Task CreatePOST(ReservationViewModel viewModel);
        Task Delete(int id);
        Task Update(Reservation reservation);
        Task<List<Reservation>> GetAllAsync(Expression<Func<Reservation, bool>>? expression=null, params string[]? include);
        Task<Reservation> GetByIdAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? include);
    }
}
