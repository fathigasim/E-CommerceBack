using EcommerceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Interfaces
{
    public interface IReadRepository<T>
    {
        Task<PaginatedList<TResult>> GetPagedAsync<TResult>(
            int pageNumber,
            int pageSize,
            System.Linq.Expressions.Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            CancellationToken cancellationToken = default
        );
    }
}
