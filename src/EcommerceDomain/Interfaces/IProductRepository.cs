using EcommerceDomain.Entities;
using MediaRTutorialDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Interfaces
{
    public interface IProductRepository : IRepository<Product>,IPagedRepository<Product>
    {

        Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(
            Guid categoryId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Product>> GetProductsByIdAsync(
          IEnumerable <Guid> prodIds, CancellationToken cancellationToken = default);
        Task<Product?> GetProductWithCategoryAsync(
            Guid productId, CancellationToken cancellationToken = default);


        //Task<(IReadOnlyList<Product>, int)> GetProductsPaginated(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
       
    }

}
