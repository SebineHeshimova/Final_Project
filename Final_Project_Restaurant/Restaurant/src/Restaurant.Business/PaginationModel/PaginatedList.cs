using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.PaginationModel
{
    public class PaginatedList<T>: List<T>
    {
        public PaginatedList(List<T> datas, int coun, int page, int pageSize)
        {
            AddRange(datas);
            ActivePage = page;
            TotalPage = (int)Math.Ceiling(coun / (double)pageSize);
        }
        public int ActivePage { get; set; }
        public int TotalPage { get; set; }
        public bool HasNext { get => ActivePage < TotalPage; }
        public bool HasPrevious { get => ActivePage > 1; }
        public static PaginatedList<T> Create(IQueryable<T> datas, int page, int pageSize)
        {
            return new PaginatedList<T>(datas.Skip((page - 1) * pageSize).Take(pageSize).ToList(), datas.ToList().Count(), page, pageSize);
        }
    }
}
