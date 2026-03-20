using AutoMapper;
using EcommerceDomain.Entities;
using EcommerceDomain.Interfaces;
using EcommerceInfrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


namespace EcommerceInfrastructure.Repository
{
    public class PaymentRepository : PagedRepository<Payment>, IPaymentRepository
    {
    

     
        public PaymentRepository(AppDbContext context, IMapper mapper) : base(context,mapper) { }
        public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _context.Payments.FindAsync(new object[] { id }, ct);

        public async Task<Payment?> GetByStripePaymentIntentIdAsync(string paymentIntentId, CancellationToken ct = default)
            => await _context.Payments
                .FirstOrDefaultAsync(p => p.StripePaymentIntentId == paymentIntentId, ct);

        public async Task<IEnumerable<Payment>> GetByUserIdAsync(string userId, CancellationToken ct = default)
            => await _context.Payments
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(ct);

        public async Task AddAsync(Payment payment, CancellationToken ct = default)
        {
            await _context.Payments.AddAsync(payment, ct);
           // await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Payment payment, CancellationToken ct = default)
        {
            _context.Payments.Update(payment);
           // await _context.SaveChangesAsync(ct);
        }
    }
}
