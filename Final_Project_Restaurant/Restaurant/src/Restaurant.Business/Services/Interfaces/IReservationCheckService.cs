using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IReservationCheckService
    {
        Task<Reservation> Detail(int id);
        Task Accept(int id);
        Task Reject(int id);
        Task Pending(int id);
        Task<List<Reservation>> GetAllAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? includes);
        Task<Reservation> GetByIdAsync(Expression<Func<Reservation, bool>>? expression = null, params string[]? includes);
    }
}
