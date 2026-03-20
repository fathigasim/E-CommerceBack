using MediaRTutorialDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Interfaces
{
 

   
        public interface IUnitOfWork //: IDisposable, IAsyncDisposable
        {
            // Repositories
            IProductRepository Products { get; }
            ICategoryRepository Categories { get; }
            IOrderRepository Orders { get; }
            IPaymentRepository Payments { get; }
            IBasketRepository Baskets { get; }

            // Transaction Management
            bool HasActiveTransaction { get; }
            Task BeginTransactionAsync(CancellationToken cancellationToken = default);
            Task CommitTransactionAsync(CancellationToken cancellationToken = default);
            Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

            // Save Changes
            Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        }
    }


