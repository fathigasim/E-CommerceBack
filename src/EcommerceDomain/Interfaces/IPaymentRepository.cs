using EcommerceDomain.Entities;
using EcommerceDomain.Interfaces;
using MediaRTutorialDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceDomain.Interfaces
{
    public interface IPaymentRepository :IRepository<Payment>, IPagedRepository<Payment>
    {
        Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Payment?> GetByStripePaymentIntentIdAsync(string paymentIntentId, CancellationToken ct = default);
        Task<IEnumerable<Payment>> GetByUserIdAsync(string userId, CancellationToken ct = default);
        Task AddAsync(Payment payment, CancellationToken cancellationToken = default);
        Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default);

      
    }
}
